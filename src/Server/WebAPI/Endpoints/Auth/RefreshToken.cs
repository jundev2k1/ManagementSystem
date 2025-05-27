// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Dtos;

namespace WebAPI.Endpoints;

public sealed class RefreshToken : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/auth/refresh-token", async (
			[FromBody] RefreshTokenRequest request,
			[FromServices] ISender sender,
			[FromServices] ILogger<Login> logger,
			CancellationToken cancellationToken) =>
		{
			var query = request.Adapt<RefreshTokenCommand>();
			var result = await sender.Send(query, cancellationToken);

			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<AuthResponseDto>.Ok(result.Adapt<AuthResponseDto>());
		}).AllowAnonymous();
	}
}
