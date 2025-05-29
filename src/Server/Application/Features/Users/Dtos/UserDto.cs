// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Users.Dtos;

public sealed class UserDto
{
	public Guid UserId { get; set; } = Guid.Empty;
	public string UserName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Avatar { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string FullName => $"{FirstName} {LastName}".Trim();
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
}
