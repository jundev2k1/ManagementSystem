// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.Pagination;

public class PaginationResult<TModel>(
	IEnumerable<TModel> items, int totalItems, int totalPages, int pageIndex, int pageSize)
	where TModel : class
{
	public IEnumerable<TModel> Items { get; } = items ?? Enumerable.Empty<TModel>();
	public int TotalItems { get; } = totalItems;
	public int TotalPages { get; } = totalPages;
	public int PageSize { get; } = pageSize;
	public int PageNumber { get; } = pageIndex;
}
