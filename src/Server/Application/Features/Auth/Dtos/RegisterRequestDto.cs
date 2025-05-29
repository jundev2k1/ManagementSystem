// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Auth.Dtos;

public sealed class RegisterRequestDto
{
	public string UserName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
}
