// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserHandler : ICommandHandler<DeleteUserCommand>
{
	private readonly IUnitOfWork _unitOfWork;

	public DeleteUserHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByIdAsync(request.UserId);
		if (targetUser is null) throw new NotFoundException("User", request.UserId);

		await _unitOfWork.Users.DeleteUserAsync(request.UserId, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
