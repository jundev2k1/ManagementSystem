// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Domain.Models;

public sealed class RefreshToken
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string? Token { get; set; }
	public Guid UserId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime ExpiresAt { get; set; }
	public bool IsRevoked { get; set; } = false;
	public DateTime? RevokedAt { get; set; }
}
