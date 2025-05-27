// Copyright(c) 2025 - Jun Dev.All rights reserved

using Application.Common.Auth;

namespace Infrastructure.Authentication;

public sealed class RefreshTokenCache : IRefreshTokenCache
{
	public RefreshTokenCache()
	{
	}

	public Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
	{
		return Task.FromResult(new RefreshToken("", Guid.NewGuid(), DateTime.UtcNow));
	}

	public Task<bool> RevokeTokenAsync(string token)
	{
		return Task.FromResult(false);
	}

	public Task<bool> ValidateRefreshTokenAsync(string token, Guid userId)
	{
		return Task.FromResult(false);
	}
}
