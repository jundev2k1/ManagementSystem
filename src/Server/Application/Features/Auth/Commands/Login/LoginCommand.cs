// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Commands.Login;

public record LoginCommand(string UserName, string Password) : ICommand<LoginResult>;

public record LoginResult(
	string UserId,
	string UserName,
	string Email,
	string FirstName,
	string LastName,
	string[] Roles,
	string Token);
