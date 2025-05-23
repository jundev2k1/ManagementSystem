// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;

namespace Domain.Models;

public sealed class RolePermission : Entity
{
	public Guid RoleId { get; set; }
	public Guid PermissionId { get; set; }
}
