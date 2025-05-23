﻿// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Dtos;

public sealed class LoginResponseDto
{
	public string UserId { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string[] Roles { get; set; } = Array.Empty<string>();
	public string Token { get; set; } = string.Empty;
}
