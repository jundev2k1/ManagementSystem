// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;

namespace Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskHandler : ICommandHandler<CreateTaskCommand, Guid>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUser _currentUser;

	public CreateTaskHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser)
	{
		_unitOfWork = unitOfWork;
		_currentUser = currentUser;
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
			CreatedBy = _currentUser.UserId.ToString(),
			LastModifiedAt = DateTime.UtcNow,
			LastModifiedBy = _currentUser.UserId.ToString()
		};
		var taskId = await _unitOfWork.Tasks.CreateNewTaskAsync(task, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return taskId;
	}
}
