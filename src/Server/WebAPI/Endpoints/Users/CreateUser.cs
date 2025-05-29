// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Dtos;

namespace WebAPI.Endpoints;

public record CreateUserResult(Guid Id);

public sealed class CreateUser : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/users", [Authorize] async (
			UserRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateUser> logger,
			CancellationToken cancellationToken) =>
		{
			var command = new CreateUserCommand(request);
			var result = await sender.Send(command, cancellationToken);
			var response = ApiResponse<CreateUserResult>.Ok(new CreateUserResult(result));

			logger.LogInformation("Response: " + JsonSerializer.Serialize(response));
			return response;
		}).RequireAuthorization();
	}
}
