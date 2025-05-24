// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services;

public sealed class CurrentUser : ICurrentUser
{
	public CurrentUser(IHttpContextAccessor httpContextAccessor)
	{
		var currentUser = httpContextAccessor.HttpContext?.User;
		if (currentUser is null) return;

		this.UserId = Guid.TryParse(currentUser.FindFirst(JwtRegisteredClaimNames.Jti)?.Value, out var userId) ? userId : Guid.Empty;
		this.Email = currentUser.FindFirst(JwtRegisteredClaimNames.Email)?.Value ?? string.Empty;
	}

	public Guid UserId { get; } = Guid.Empty;
	public string Email { get; } = string.Empty;
}
