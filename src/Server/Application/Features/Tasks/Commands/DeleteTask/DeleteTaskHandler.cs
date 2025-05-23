// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Commands.DeleteTask;

public sealed class DeleteTaskHandler : ICommandHandler<DeleteTaskCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteTaskHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
	{
		var targetTask = await _unitOfWork.Tasks.GetTaskByIdAsync(request.TaskId);
		if (targetTask is null) throw new NotFoundException("Task", request.TaskId);

		await _unitOfWork.Tasks.DeleteTaskAsync(request.TaskId, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
