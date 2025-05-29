// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetTaskById;

namespace WebAPI.Endpoints;

public sealed class GetUserById : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/users/{userId:guid}", async (
			[FromRoute] Guid userId,
			[FromServices] ISender sender,
			[FromServices] ILogger<GetUserById> logger,
			CancellationToken cancellationToken) =>
		{
			var result = await sender.Send(new GetUserByIdQuery(userId), cancellationToken);
			var response = ApiResponse<UserDto>.Ok(result);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(response));

			return response;
		}).RequireAuthorization();
	}
}
