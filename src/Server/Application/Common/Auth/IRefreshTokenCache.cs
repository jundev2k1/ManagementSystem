// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Application.Common.Auth;

public interface IRefreshTokenCache
{
	Task SaveAsync(RefreshToken token, CancellationToken cancellationToken = default);

	Task<RefreshToken?> GetAsync(string token, CancellationToken cancellationToken = default);

	Task RevokeAsync(string token, CancellationToken cancellationToken = default);

	Task DeleteAsync(string token, CancellationToken cancellationToken = default);
}
