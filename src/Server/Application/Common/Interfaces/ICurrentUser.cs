// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Interfaces;

public interface ICurrentUser
{
	Guid UserId { get; }
	string Email { get; }
}
