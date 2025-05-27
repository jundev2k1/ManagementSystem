// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces;

public interface IRedisCache
{
	Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
	Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default);
	Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}