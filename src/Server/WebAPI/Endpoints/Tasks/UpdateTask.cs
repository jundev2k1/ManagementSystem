// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Commands.UpdateTask;

namespace WebAPI.Endpoints;

public sealed class UpdateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/tasks/{taskId:guid}", async (
			[FromRoute] Guid taskId,
			TaskInfo request,
			[FromServices] ISender sender,
			[FromServices] ILogger<CreateTask> logger,
			CancellationToken cancellationToken) =>
		{
			if (request.TaskId != taskId)
			{
				logger.LogError("Task ID in the request does not match the Task ID in the URL.");
				throw new BadRequestException("Task ID mismatch.");
			}

			var result = await sender.Send(new UpdateTaskCommand(request), cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<object?>.Ok();
		}).RequireAuthorization();
	}
}
