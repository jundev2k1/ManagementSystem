// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Domain.Enums;

namespace Application.Features.Tasks.Queries.GetByCriteria;

public sealed class GetByCriteriaQuery : IQuery<PaginationResult<TaskInfo>>
{
	public DateTime? StartDateFrom { get; set; }
	public DateTime? StartDateTo { get; set; }
	public DateTime? DueDateFrom { get; set; }
	public DateTime? DueDateTo { get; set; }
	public TaskStatusEnum? Status { get; set; }
	public string? Keyword { get; set; }
	public int? Page { get; set; } = 1;
	public int? PageSize { get; set; } = 20;
	public string? SortBy { get; set; }
}
