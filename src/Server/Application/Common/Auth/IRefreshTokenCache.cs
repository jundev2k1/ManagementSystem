// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Application.Common.Auth;

public interface IRefreshTokenCache
{
	Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId);

	Task<bool> RevokeTokenAsync(string token);

	Task<bool> ValidateRefreshTokenAsync(string token, Guid userId);
}
