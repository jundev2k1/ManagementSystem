// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Data.Caching;

public class RedisCache(IDistributedCache distributedCache) : IRedisCache
{
	public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
	{
		var value = await distributedCache.GetStringAsync(key, cancellationToken);
		return value is null ? default : JsonSerializer.Deserialize<T>(value);
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
	{
		var options = new DistributedCacheEntryOptions();
		if (expirationTime != null)
			options.AbsoluteExpirationRelativeToNow = expirationTime;

		var jsonValue = JsonSerializer.Serialize(value);
		await distributedCache.SetStringAsync(key, jsonValue, options, cancellationToken);
	}

	public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
	{
		await distributedCache.RemoveAsync(key, cancellationToken);
	}
}