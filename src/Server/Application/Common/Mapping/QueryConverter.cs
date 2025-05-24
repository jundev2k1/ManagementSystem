// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Filters;

namespace Application.Common.Mapping;

public static class QueryConverter
{
	public static IEnumerable<QueryFilter> ToFilters(
		IEnumerable<string>? filters,
		string? searchText = null)
	{
		// Create filters from the provided string collection
		if ((filters != null) && filters.Any())
		{
			foreach (var filter in filters)
			{
				var parts = filter.Split("~");
				if (parts.Length != 3) continue;

				yield return new QueryFilter(
					Field: parts[0],
					Operator: parts[1],
					Value: parts[2]);
			}
		}

		// Add full text search filter if searchText is provided
		if (searchText is not null)
		{
			yield return new QueryFilter(
				Field: "fullText",
				Operator: string.Empty,
				Value: searchText);
		}
	}

	public static IEnumerable<QuerySort> ToSorts(IEnumerable<string>? sorts)
	{
		if (sorts is null || !sorts.Any()) yield break;

		// Create sorts from the provided string collection
		foreach (var sort in sorts)
		{
			var parts = sort.Split(":");
			if (parts.Length != 2) continue;

			yield return new QuerySort(
				Field: parts[0],
				Direction: parts[1]);
		}
	}
}
