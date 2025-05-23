// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;

namespace Domain.Models;

public sealed class Permission : Entity
{
	public Guid PermissionId { get; set; }
	public string PermissionName { get; set; } = string.Empty;
	public string DisplayName { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public string Action { get; set; } = string.Empty;
	public bool ValidFlg { get; set; } = true;
}
