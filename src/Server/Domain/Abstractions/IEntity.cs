// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Domain.Abstractions;

public interface IEntity
{
	DateTime? CreatedAt { get; set; }
	string? CreatedBy { get; set; }
	DateTime? LastModifiedAt { get; set; }
	string? LastModifiedBy { get; set; }
}
