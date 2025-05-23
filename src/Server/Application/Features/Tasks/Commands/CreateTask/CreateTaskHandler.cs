// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskHandler : ICommandHandler<CreateTaskCommand, Guid>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateTaskHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
	{
		var task = new TaskInfo
		{
			TaskId = Guid.NewGuid(),
			Title =  request.Task.Title,
			Description = request.Task.Description,
			Status = request.Task.Status,
			Progress = request.Task.Progress,
			StartDate = request.Task.StartDate,
			DueDate = request.Task.DueDate,
			Priority = request.Task.Priority,
			AssignedTo = request.Task.AssignedTo,
			AssignedBy = request.Task.AssignedBy,
			Note = request.Task.Note,
			CreatedAt = DateTime.UtcNow,
			CreatedBy = request.Task.CreatedBy,
			LastModifiedAt = DateTime.UtcNow,
			LastModifiedBy = request.Task.LastModifiedBy
		};
		var taskId = await _unitOfWork.Tasks.CreateNewTaskAsync(task, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return taskId;
	}
}
