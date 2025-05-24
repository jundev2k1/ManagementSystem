// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Application.Features.Auth.Dtos;
using Shared.Constants;

namespace Application.Features.Auth.Commands.Register;

public sealed class RegisterHandler : ICommandHandler<RegisterCommand, RegisterResult>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordHasher _passwordHasher;

	public RegisterHandler(
		IJwtTokenGenerator jwtTokenGenerator,
		IUnitOfWork unitOfWork,
		IPasswordHasher passwordHasher)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_unitOfWork = unitOfWork;
		_passwordHasher = passwordHasher;
	}

	public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByUserNameAsync(request.UserInput.UserName, cancellationToken);
		if (targetUser != null) throw new ValidationException();

		var newUser = new User
		{
			UserId = Guid.NewGuid(),
			UserName = request.UserInput.UserName,
			Email = request.UserInput.Email,
			PasswordHash = _passwordHasher.HashPassword(request.UserInput.Password),
			FirstName = request.UserInput.FirstName,
			LastName = request.UserInput.LastName,
			PhoneNumber = request.UserInput.PhoneNumber,
			Address = request.UserInput.Address,
			Avatar = string.Empty,
			ValidFlg = true,
			CreatedAt = DateTime.UtcNow,
			CreatedBy = Constants.AuditTags.SYSTEM,
			LastModifiedAt = DateTime.UtcNow,
			LastModifiedBy = Constants.AuditTags.SYSTEM,
		};
		var userId = await _unitOfWork.Users.CreateNewUserAsync(newUser, cancellationToken);
		await _unitOfWork.SaveAsync(cancellationToken);

		var user = await _unitOfWork.Users.GetUserByIdAsync(userId, cancellationToken);
		if (user == null) throw new NotFoundException("User not found after creation.");

		var token = _jwtTokenGenerator.GenerateJwtToken(user.UserId, user.Email, Array.Empty<string>());
		var result = new RegisterResult(
			UserId: user.UserId.ToString(),
			UserName: user.UserName,
			Email: user.Email,
			FirstName: user.FirstName ?? string.Empty,
			LastName: user.LastName ?? string.Empty,
			Roles: Array.Empty<string>(),
			Token: token);
		return result;
	}
}
