// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Features.Auth.Commands.Register;

public sealed class RegisterHandler : ICommandHandler<RegisterCommand, RegisterResult>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordHasher _passwordHasher;
	private readonly ILogger<RegisterHandler> _logger;

	public RegisterHandler(
		IJwtTokenGenerator jwtTokenGenerator,
		IUnitOfWork unitOfWork,
		IPasswordHasher passwordHasher,
		ILogger<RegisterHandler> logger)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_unitOfWork = unitOfWork;
		_passwordHasher = passwordHasher;
		_logger = logger;
	}

	public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		// Validate user input in DB
		var isValidData = await ValidateUserInputAsync(request, cancellationToken);
		if (!isValidData) throw new ValidationException();

		// Create new user
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

		// Generate JWT token for the new user
		var token = _jwtTokenGenerator.GenerateJwtToken(userId, newUser.Email, Array.Empty<string>());

		// Return the registration result
		var result = new RegisterResult(
			UserId: userId.ToString(),
			UserName: newUser.UserName,
			Email: newUser.Email,
			FirstName: newUser.FirstName ?? string.Empty,
			LastName: newUser.LastName ?? string.Empty,
			Roles: Array.Empty<string>(),
			Token: token);
		return result;
	}

	private async Task<bool> ValidateUserInputAsync(RegisterCommand request, CancellationToken cancellationToken)
	{
		var isExistsUserName = await _unitOfWork.Users.IsExistsUserName(request.UserInput.UserName, cancellationToken);
		if (isExistsUserName)
		{
			_logger.LogWarning("Username already exists: {UserName}", request.UserInput.UserName);
			return false;
		}

		var isExistsEmail = await _unitOfWork.Users.IsExistsEmail(request.UserInput.Email, cancellationToken);
		if (isExistsEmail)
		{
			_logger.LogWarning("Email already exists: {Email}", request.UserInput.Email);
			return false;
		}

		return true;
	}
}
