// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;

namespace Domain.Models;

public sealed class UserRole : Entity
{
	public Guid UserId { get; set; }
	public Guid RoleId { get; set; }
	public DateTime? AssignedAt { get; set; }
	public string AssignedBy { get; set; } = string.Empty;
}
