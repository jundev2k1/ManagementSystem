// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;

namespace Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserHandler : ICommandHandler<UpdateUserCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUser _currentUser;
	private readonly IPasswordHasher _passwordHasher;

	public UpdateUserHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser, IPasswordHasher passwordHasher)
	{
		_unitOfWork = unitOfWork;
		_currentUser = currentUser;
		_passwordHasher = passwordHasher;
	}

	public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByIdAsync(request.User.UserId);
		if (targetUser is null) throw new NotFoundException("User", request.User.UserId);

		if (request.User.Password != null)
			targetUser.PasswordHash = _passwordHasher.HashPassword(request.User.Password);

		targetUser.Email = request.User.Email;
		targetUser.FirstName = request.User.FirstName;
		targetUser.LastName = request.User.LastName;
		targetUser.PhoneNumber = request.User.PhoneNumber;
		targetUser.Address = request.User.Address;
		targetUser.LastModifiedAt = DateTime.UtcNow;
		targetUser.LastModifiedBy = _currentUser.UserId.ToString();
		await _unitOfWork.Users.UpdateUserAsync(targetUser, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
