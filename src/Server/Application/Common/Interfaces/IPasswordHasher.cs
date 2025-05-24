// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces;

public interface IPasswordHasher
{
	string HashPassword(string password);

	bool VerifyPassword(string hashedPassword, string providedPassword);
}
