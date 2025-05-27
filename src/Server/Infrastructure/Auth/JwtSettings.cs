// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Infrastructure.Auth;

public sealed class JwtSettings
{
	public string? SecretKey { get; set; }
	public string? Issuer { get; set; }
	public string? Audience { get; set; }
	public int ExpiryMinutes { get; set; } = 60;
}
