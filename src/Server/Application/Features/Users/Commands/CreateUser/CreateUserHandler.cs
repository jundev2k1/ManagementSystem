// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;

namespace Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserHandler : ICommandHandler<CreateUserCommand, Guid>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUser _currentUser;
	private readonly IPasswordHasher _passwordHasher;

	public CreateUserHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser, IPasswordHasher passwordHasher)
	{
		_unitOfWork = unitOfWork;
		_currentUser = currentUser;
		_passwordHasher = passwordHasher;
	}

	public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	{
		var user = request.User.Adapt<User>();
		user.UserId = Guid.NewGuid();
		user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
		user.CreatedAt = DateTime.UtcNow;
		user.CreatedBy = _currentUser.UserId.ToString();
		user.LastModifiedAt = DateTime.UtcNow;
		user.LastModifiedBy = _currentUser.UserId.ToString();

		var userId = await _unitOfWork.Users.CreateNewUserAsync(user, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		return userId;
	}
}
