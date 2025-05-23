// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Domain.Enums;

namespace WebAPI.Dtos;

public sealed class TaskSearchCriteriaRequest : PaginationRequest
{
	public DateTime? StartDateFrom { get; set; }
	public DateTime? StartDateTo { get; set; }
	public DateTime? DueDateFrom { get; set; }
	public DateTime? DueDateTo { get; set; }
	public TaskStatusEnum? Status { get; set; }
}
