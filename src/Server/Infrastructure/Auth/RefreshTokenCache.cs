// Copyright(c) 2025 - Jun Dev.All rights reserved

using Application.Common.Auth;
using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Authentication;

public sealed class RefreshTokenCache : IRefreshTokenCache
{
	private readonly IRedisCache _redisCache;
	private readonly int _expirationDays;

	public RefreshTokenCache(IRedisCache redisCache, IConfiguration configuration)
	{
		_redisCache = redisCache;
		_expirationDays = configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryDays", 30);
	}

	public async Task SaveAsync(RefreshToken token, CancellationToken cancellationToken = default)
	{
		await _redisCache.SetAsync(token.Token, token, TimeSpan.FromDays(_expirationDays), cancellationToken);
	}

	public async Task<RefreshToken?> GetAsync(string token, CancellationToken cancellationToken = default)
	{
		return await _redisCache.GetAsync<RefreshToken>(token, cancellationToken);
	}

	public async Task RevokeAsync(string token, CancellationToken cancellationToken = default)
	{
		var refreshToken = await GetAsync(token, cancellationToken);
		if (refreshToken is not null)
		{
			refreshToken.IsRevoked = true;
			await SaveAsync(refreshToken, cancellationToken);
		}
	}

	public async Task DeleteAsync(string token, CancellationToken cancellationToken = default)
	{
		await _redisCache.RemoveAsync(token, cancellationToken);
	}
}
