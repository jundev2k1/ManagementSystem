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
		return await Task.FromResult(Unit.Value);
	}
}
