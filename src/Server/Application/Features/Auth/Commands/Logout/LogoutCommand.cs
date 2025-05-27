// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken) : ICommand;

