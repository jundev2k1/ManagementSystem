// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Filters;

namespace Application.Features.Tasks.Queries.GetByCriteria;

public sealed class GetsByCriteriaHandler : IQueryHandler<GetByCriteriaQuery, PaginationResult<TaskInfo>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICriteriaBuilder<TaskInfo> _criteriaBuilder;

	public GetsByCriteriaHandler(IUnitOfWork unitOfWork, ICriteriaBuilder<TaskInfo> criteriaBuilder)
	{
		_unitOfWork = unitOfWork;
		_criteriaBuilder = criteriaBuilder;
	}

	public async Task<PaginationResult<TaskInfo>> Handle(GetByCriteriaQuery request, CancellationToken cancellationToken)
	{
		var result = await _unitOfWork.Tasks.GetTasksByCriteriaAsync(
			queryBuilder: query => _criteriaBuilder.Apply(query, request.Filters, request.Sorts),
			page: request.PageNumber ?? 1,
			pageSize: request.PageSize ?? 20,
			cancellationToken: cancellationToken);
		return result;
	}
}
