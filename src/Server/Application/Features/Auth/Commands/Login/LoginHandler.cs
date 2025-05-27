// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Auth;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;

namespace Application.Features.Auth.Commands.Login;

public sealed class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
{
	private readonly IAuthService _authService;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IPasswordHasher _passwordHasher;

	public LoginHandler(
		IAuthService authService,
		IUnitOfWork unitOfWork,
		IPasswordHasher passwordHasher)
	{
		_authService = authService;
		_unitOfWork = unitOfWork;
		_passwordHasher = passwordHasher;
	}

	public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		var targetUser = await _unitOfWork.Users.GetUserByUserNameAsync(request.UserName, cancellationToken);
		if (targetUser is null || !_passwordHasher.VerifyPassword(targetUser.PasswordHash, request.Password))
			throw new UnauthorizedAccessException("Invalid email or password.");

		var refreshToken = await _authService.GenerateRefreshTokenAsync(targetUser.UserId);
		var accessToken = _authService.GenerateAccessToken(targetUser.UserId, targetUser.Email, Array.Empty<string>());
		var result = new LoginResult(
			UserId: targetUser.UserId.ToString(),
			UserName: targetUser.UserName,
			Email: targetUser.Email,
			FirstName: targetUser.FirstName ?? string.Empty,
			LastName: targetUser.LastName ?? string.Empty,
			Roles: Array.Empty<string>(),
			AccessToken: accessToken,
			RefreshToken: refreshToken.Token);
		return result;
	}
}
