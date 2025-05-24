// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Dtos;

namespace WebAPI.Endpoints;

public sealed class Register : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/auth/register", async (
			[FromBody] RegisterRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<Login> logger,
			CancellationToken cancellationToken) =>
		{
			var query = new RegisterCommand(request.Adapt<RegisterRequestDto>());
			var result = await sender.Send(query, cancellationToken);

			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<AuthResponseDto>.Ok(result.Adapt<AuthResponseDto>());
		}).AllowAnonymous();
	}
}
