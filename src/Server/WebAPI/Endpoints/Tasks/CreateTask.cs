// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Commands.CreateTask;

namespace WebAPI.Endpoints;

public record CreateTaskResult(Guid Id);

public sealed class CreateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/tasks", async (
			TaskInfo request,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateTask> logger,
			CancellationToken cancellationToken) =>
		{
			var result = await sender.Send(new CreateTaskCommand(request), cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<CreateTaskResult>.Ok(new CreateTaskResult(result));
		});
	}
}
