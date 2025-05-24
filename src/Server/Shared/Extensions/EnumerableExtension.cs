// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Shared.Extensions;

public static class EnumerableExtension
{
	public static string JoinToString<T>(this IEnumerable<T> input, string separator)
	{
		return input is not null
			? string.Join(separator, input)
			: string.Empty;
	}
}
