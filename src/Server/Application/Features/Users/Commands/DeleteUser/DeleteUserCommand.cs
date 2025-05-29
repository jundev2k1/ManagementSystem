// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : ICommand;
