// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Dtos;

public sealed class SearchCriteriaRequest
{
	[FromQuery(Name = "search")]
	public string? Keyword { get; set; }
	[FromQuery(Name = "filter")]
	public string[]? Filters { get; set; } = Array.Empty<string>();
	[FromQuery(Name = "sort")]
	public string[]? Sorts { get; set; } = Array.Empty<string>();
	[FromQuery(Name = "page")]
	public int? PageNumber { get; set; } = 1;
	[FromQuery(Name = "pageSize")]
	public int? PageSize { get; set; } = 20;
}
