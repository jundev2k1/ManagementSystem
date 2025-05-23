// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Queries.GetTaskById;

public sealed class GetTaskByIdHandler : IQueryHandler<GetTaskByIdQuery, TaskInfo>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetTaskByIdHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<TaskInfo> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
	{
		var targetTask = await _unitOfWork.Tasks.GetTaskByIdAsync(request.TaskId, cancellationToken);
		if (targetTask is null) throw new NotFoundException("Task", request.TaskId);

		return targetTask;
	}
}
