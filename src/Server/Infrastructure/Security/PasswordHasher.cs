// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security;

public sealed class PasswordHasherService : IPasswordHasher
{
	private readonly PasswordHasher<object> _hasher;
	public PasswordHasherService()
	{
		_hasher = new PasswordHasher<object>();
	}

	public string HashPassword(string password) =>
		_hasher.HashPassword(null!, password);

	public bool VerifyPassword(string hashedPassword, string providedPassword)
	{
		var result = _hasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
		return (result == PasswordVerificationResult.Success)
			|| (result == PasswordVerificationResult.SuccessRehashNeeded);
	}
}
