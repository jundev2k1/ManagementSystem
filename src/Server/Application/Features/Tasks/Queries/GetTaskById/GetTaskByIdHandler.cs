// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Queries.GetTaskById;

public sealed class GetTaskByIdHandler : IQueryHandler<GetTaskByIdQuery, TaskInfo>
{
	public GetTaskByIdHandler()
	{
	}

	public async Task<TaskInfo> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
	{
		// Simulate fetching task info from a database or other source
		var taskInfo = new TaskInfo
		{
			TaskId = request.TaskId,
			Title = "Sample Task",
			Description = "This is a sample task description.",
			Status = TaskStatusEnum.InProgress,
			CreatedAt = DateTime.UtcNow,
			CreatedBy = "admin"
		};
		return await Task.FromResult(taskInfo);
	}
}
