// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;

namespace Application.Features.Auth.Commands.Login;

public sealed class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
{
	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordHasher _passwordHasher;

	public LoginHandler(
		IJwtTokenGenerator jwtTokenGenerator,
		IUnitOfWork unitOfWork,
		IPasswordHasher passwordHasher)
	{
		_jwtTokenGenerator = jwtTokenGenerator;
		_unitOfWork = unitOfWork;
		_passwordHasher = passwordHasher;
	}

	public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByUsernameAsync(request.Username, cancellationToken);
		if (targetUser is null || !_passwordHasher.VerifyPassword(request.Password, targetUser.PasswordHash))
			throw new UnauthorizedAccessException("Invalid email or password.");

		var token = _jwtTokenGenerator.GenerateJwtToken(targetUser.UserId, targetUser.Email, Array.Empty<string>());
		var result = new LoginResult(
			UserId: targetUser.UserId,
			UserName: targetUser.UserName,
			FirstName: targetUser.FirstName ?? string.Empty,
			LastName: targetUser.LastName ?? string.Empty,
			Token: token,
			Roles: Array.Empty<string>());
		return result;
	}
}
