// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Filters;

namespace Application.Features.Users.Queries.GetByCriteria;

public record GetByCriteriaQuery(
	IEnumerable<QueryFilter> Filters,
	IEnumerable<QuerySort>? Sorts,
	int? PageNumber,
	int? PageSize)
	: IQuery<PaginationResult<User>>;
