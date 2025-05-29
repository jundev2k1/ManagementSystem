// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Dtos;

namespace Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(UserRequestDto User) : ICommand<Guid>;
