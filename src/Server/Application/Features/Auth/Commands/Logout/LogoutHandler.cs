// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;

namespace Application.Features.Auth.Commands.Logout;

public sealed class LogoutHandler : ICommandHandler<LogoutCommand>
{
	private readonly IAuthService _authService;
	private readonly ICurrentUser _currentUser;

	public LogoutHandler(
		IAuthService authService,
		ICurrentUser currentUser)
	{
		_authService = authService;
		_currentUser = currentUser;
	}

	public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
	{
		var isValidToken = await _authService.ValidateRefreshTokenAsync(request.RefreshToken, _currentUser.UserId, cancellationToken);
		if (!isValidToken)
			throw new UnauthorizedAccessException("Invalid or expired refresh token.");

		await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);
		return Unit.Value;
	}
}
