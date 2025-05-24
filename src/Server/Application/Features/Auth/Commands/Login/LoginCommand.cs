// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Commands.Login;

public record LoginCommand(string Username, string Password) : ICommand<LoginResult>;

public record LoginResult(
	Guid UserId,
	string UserName,
	string FirstName,
	string LastName,
	string[] Roles,
	string Token);
