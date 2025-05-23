// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskHandler : ICommandHandler<UpdateTaskCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateTaskHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
	{
		await _unitOfWork.Tasks.UpdateTaskAsync(request.Task, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
