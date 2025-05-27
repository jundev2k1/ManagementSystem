// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Dtos;

namespace WebAPI.Endpoints;

public sealed class Login : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/auth/login", async (
			[FromBody] AuthRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<Login> logger,
			CancellationToken cancellationToken) =>
		{
			var query = request.Adapt<LoginCommand>();
			var result = await sender.Send(query, cancellationToken);

			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<AuthResponseDto>.Ok(result.Adapt<AuthResponseDto>());
		}).AllowAnonymous();
	}
}
