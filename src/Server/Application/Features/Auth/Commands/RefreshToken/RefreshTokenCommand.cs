// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResult>;

public record RefreshTokenResult(
	string UserId,
	string UserName,
	string Email,
	string FirstName,
	string LastName,
	string[] Roles,
	string AccessToken,
	string RefreshToken);
