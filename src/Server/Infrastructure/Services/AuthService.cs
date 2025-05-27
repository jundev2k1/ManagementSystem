// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Auth;
using Application.Common.Interfaces.Services;
using Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Infrastructure.Services;

public sealed class AuthService : IAuthService
{

	private readonly IRefreshTokenCache _refreshTokenCache;
	private readonly IJwtTokenGenerator _tokenGenerator;
	private readonly JwtSettings _jwtSettings;

	public AuthService(
		IRefreshTokenCache refreshTokenCache,
		IJwtTokenGenerator tokenGenerator,
		IConfiguration configuration)
	{
		_refreshTokenCache = refreshTokenCache;
		_tokenGenerator = tokenGenerator;
		_jwtSettings = configuration.GetSection("JwtSettings")
			.Get<JwtSettings>() ?? new JwtSettings();
	}

	public string GenerateAccessToken(Guid userId, string email, string[] roles)
	{
		return _tokenGenerator.GenerateJwtToken(userId, email, roles);
	}

	public async Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		var token = new RefreshToken
		{
			Token = GenerateRefreshToken(),
			UserId = userId,
			ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays)
		};

		await _refreshTokenCache.SaveAsync(token);
		return token;
	}

	public async Task<bool> RevokeTokenAsync(string token, CancellationToken cancellationToken = default)
	{
		var existing = await _refreshTokenCache.GetAsync(token);
		if (existing == null || existing.IsRevoked || existing.IsExpired)
			return false;

		await _refreshTokenCache.RevokeAsync(token);
		return true;
	}

	public async Task<bool> ValidateRefreshTokenAsync(string token, Guid userId, CancellationToken cancellationToken = default)
	{
		var existing = await _refreshTokenCache.GetAsync(token);
		var isValidToken = existing != null
			&& !existing.IsRevoked
			&& !existing.IsExpired
			&& existing.UserId == userId;
		return isValidToken;
	}

	private string GenerateRefreshToken(int size = 64)
	{
		var randomNumber = new byte[size];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}
}
