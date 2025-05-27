// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Auth;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Services;

namespace Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenCommand, RefreshTokenResult>
{
	private readonly IAuthService _authService;
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICurrentUser _currentUser;

	public RefreshTokenHandler(
		IAuthService authService,
		IUnitOfWork unitOfWork,
		ICurrentUser currentUser)
	{
		_authService = authService;
		_unitOfWork = unitOfWork;
		_currentUser = currentUser;
	}

	public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
	{
		var isValidToken = await _authService.ValidateRefreshTokenAsync(request.RefreshToken, _currentUser.UserId, cancellationToken);
		if (!isValidToken) throw new UnauthorizedAccessException("Invalid or expired refresh token.");

		var targetUser = await _unitOfWork.Users.GetUserByIdAsync(_currentUser.UserId, cancellationToken);
		if (targetUser == null) throw new UnauthorizedAccessException("Invalid user.");

		await _authService.RevokeTokenAsync(request.RefreshToken, cancellationToken);
		var refreshToken = await _authService.GenerateRefreshTokenAsync(targetUser.UserId);
		var accessToken = _authService.GenerateAccessToken(targetUser.UserId, targetUser.Email, Array.Empty<string>());
		var result = new RefreshTokenResult(
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
