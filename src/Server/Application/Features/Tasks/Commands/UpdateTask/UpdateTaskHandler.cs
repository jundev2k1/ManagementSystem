// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;

namespace Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskHandler : ICommandHandler<UpdateTaskCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUser _currentUser;

	public UpdateTaskHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser)
	{
		_unitOfWork = unitOfWork;
		_currentUser = currentUser;
	}

	public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
	{
		request.Task.LastModifiedAt = DateTime.UtcNow;
		request.Task.LastModifiedBy = _currentUser.UserId.ToString();
		await _unitOfWork.Tasks.UpdateTaskAsync(request.Task, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
