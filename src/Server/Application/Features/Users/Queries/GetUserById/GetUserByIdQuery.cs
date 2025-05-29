// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Dtos;

namespace Application.Features.Users.Queries.GetTaskById;

public record GetUserByIdQuery(Guid UserId) : IQuery<UserDto>;
