// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Commands.DeleteTask;

namespace WebAPI.Endpoints;

public sealed class DeleteTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/tasks/{taskId:guid}", async (
			[FromRoute] Guid taskId,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateTask> logger,
			CancellationToken cancellationToken) =>
		{
			logger.LogInformation("Endpoint => Creating task: {TaskId}", taskId);

			var result = await sender.Send(new DeleteTaskCommand(taskId), cancellationToken);
			logger.LogInformation("Response:  => " + JsonSerializer.Serialize(result));

			return Results.Ok();
		});
	}
}
