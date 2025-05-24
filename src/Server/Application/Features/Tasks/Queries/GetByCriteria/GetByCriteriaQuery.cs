// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Filters;
using Domain.Enums;

namespace Application.Features.Tasks.Queries.GetByCriteria;

public record GetByCriteriaQuery(
	IEnumerable<QueryFilter> Filters,
	IEnumerable<QuerySort>? Sorts,
	int? PageNumber,
	int? PageSize)
	: IQuery<PaginationResult<TaskInfo>>;
