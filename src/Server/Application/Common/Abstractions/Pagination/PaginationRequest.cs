// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.Pagination;

public class PaginationRequest
{
	public string? Keyword { get; set; }
	public int? Page { get; set; } = 1;
	public int? PageSize { get; set; } = 20;
	public string? SortBy { get; set; }
}
