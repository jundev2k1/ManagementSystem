// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
	string GenerateJwtToken(Guid userId, string email, string[] roles);
}
