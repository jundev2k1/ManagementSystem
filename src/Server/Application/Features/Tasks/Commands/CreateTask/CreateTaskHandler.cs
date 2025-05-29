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
		var task = request.Task.Adapt<TaskInfo>();
		task.TaskId = Guid.NewGuid();
		task.CreatedAt = DateTime.UtcNow;
		task.CreatedBy = _currentUser.UserId.ToString();
		task.LastModifiedAt = DateTime.UtcNow;
		task.LastModifiedBy = _currentUser.UserId.ToString();

		var taskId = await _unitOfWork.Tasks.CreateNewTaskAsync(task, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return taskId;
	}
}
