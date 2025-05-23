// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;

namespace Domain.Models;

public sealed class Role : Entity
{
	public Guid RoleId { get; set; }
	public string RoleName { get; set; } = string.Empty;
	public string DisplayName { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public bool ValidFlg { get; set; } = true;
	public bool IsSystem { get; set; } = true;
}
