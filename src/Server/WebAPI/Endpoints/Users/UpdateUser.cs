// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Dtos;

namespace WebAPI.Endpoints;

public sealed class UpdateUser : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/users/{userId:guid}", async (
			[FromRoute] Guid userId,
			UserRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateUser> logger,
			CancellationToken cancellationToken) =>
		{
			if (request.UserId != userId)
			{
				logger.LogError("User ID in the request does not match the User ID in the URL.");
				throw new BadRequestException("User ID mismatch.");
			}

			var command = new UpdateUserCommand(request);
			var result = await sender.Send(command, cancellationToken);

			return ApiResponse<object?>.Ok();
		}).RequireAuthorization();
	}
}
