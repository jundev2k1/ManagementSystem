// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Filters;

public interface ICriteriaBuilder<TModel>
{
	IQueryable<TModel> Apply(
		IQueryable<TModel> query,
		IEnumerable<QueryFilter> filters,
		IEnumerable<QuerySort>? sorts = null);
}
