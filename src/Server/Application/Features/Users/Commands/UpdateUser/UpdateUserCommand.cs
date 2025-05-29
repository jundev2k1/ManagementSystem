// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Dtos;

namespace Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(UserRequestDto User) : ICommand;
