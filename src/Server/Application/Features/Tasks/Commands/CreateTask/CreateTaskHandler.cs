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
		return await Task.FromResult(Guid.NewGuid());
	}
}
