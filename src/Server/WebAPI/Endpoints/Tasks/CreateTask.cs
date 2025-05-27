// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Commands.CreateTask;
using Application.Features.Tasks.Dtos;

namespace WebAPI.Endpoints;

public record CreateTaskResult(Guid Id);

public sealed class CreateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/tasks", [Authorize] async (
			TaskInfoRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateTask> logger,
			CancellationToken cancellationToken) =>
		{
			var command = new CreateTaskCommand(request.Adapt<TaskInfo>());
			var result = await sender.Send(command, cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<CreateTaskResult>.Ok(new CreateTaskResult(result));
		}).RequireAuthorization();
	}
}
