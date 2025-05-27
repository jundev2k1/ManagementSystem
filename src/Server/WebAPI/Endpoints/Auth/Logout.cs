// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Auth.Commands.Logout;

namespace WebAPI.Endpoints;

public sealed class Logout : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/auth/logout", async (
			[FromBody] RefreshTokenRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<Login> logger,
			CancellationToken cancellationToken) =>
		{
			var query = request.Adapt<LogoutCommand>();
			var result = await sender.Send(query, cancellationToken);

			return ApiResponse<object?>.Ok();
		}).RequireAuthorization();
	}
}
