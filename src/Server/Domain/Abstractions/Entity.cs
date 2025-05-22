// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Domain.Abstractions;

public class Entity : IEntity
{
	public DateTime? CreatedAt { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime? LastModifiedAt { get; set; }
	public string? LastModifiedBy { get; set; }
}
