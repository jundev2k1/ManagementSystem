// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces.Services;

public interface IAuthService
{
	string GenerateAccessToken(Guid userId, string email, string[] roles);

	Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId, CancellationToken cancellationToken = default);

	Task<bool> RevokeTokenAsync(string token, CancellationToken cancellationToken = default);

	Task<bool> ValidateRefreshTokenAsync(string token, Guid userId, CancellationToken cancellationToken = default);
}
