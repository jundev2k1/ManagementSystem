// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Auth.Dtos;

namespace Application.Features.Auth.Commands.Register;

public record RegisterCommand(RegisterRequestDto UserInput) : ICommand<RegisterResult>;

public record RegisterResult(
	string UserId,
	string UserName,
	string Email,
	string FirstName,
	string LastName,
	string[] Roles,
	string Token);
