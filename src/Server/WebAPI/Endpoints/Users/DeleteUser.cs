// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Commands.DeleteUser;

namespace WebAPI.Endpoints;

public sealed class DeleteUser : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/users/{userId:guid}", async (
			[FromRoute] Guid userId,
			[FromServices] ISender sender,
			CancellationToken cancellationToken) =>
		{
			var result = await sender.Send(new DeleteUserCommand(userId), cancellationToken);

			return ApiResponse<object?>.Ok();
		}).RequireAuthorization();
	}
}
