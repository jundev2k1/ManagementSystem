// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;

namespace Domain.Models;

public sealed class User : Entity
{
	public Guid UserId { get; set; }
	public string UserName { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string PasswordHash { get; set; } = string.Empty;
	public string Avatar { get; set; } = string.Empty;
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public bool ValidFlg { get; set; } = true;
}
