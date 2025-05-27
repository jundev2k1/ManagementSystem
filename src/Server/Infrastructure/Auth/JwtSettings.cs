// Copyright(c) 2025 - Jun Dev.All rights reserved

namespace Infrastructure.Auth;

public sealed class JwtSettings
{
	public string? SecretKey { get; set; }
	public string? Issuer { get; set; }
	public string? Audience { get; set; }
	public int AccessTokenExpiryMinutes { get; set; } = 20;
	public int RefreshTokenExpiryDays { get; set; } = 30;
}
