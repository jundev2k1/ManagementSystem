// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;

namespace Application.Features.Tasks.Queries.GetByCriteria;

public sealed class GetByCriteriaHandler : IQueryHandler<GetByCriteriaQuery, PaginationResult<TaskInfo>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetByCriteriaHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<PaginationResult<TaskInfo>> Handle(GetByCriteriaQuery request, CancellationToken cancellationToken)
	{
		var dummy = new PaginationResult<TaskInfo>(
			items: Array.Empty<TaskInfo>(),
			totalItems: 0,
			totalPages: 1,
			pageIndex: 1,
			pageSize: 20);
		return await Task.FromResult(dummy);
	}
}
