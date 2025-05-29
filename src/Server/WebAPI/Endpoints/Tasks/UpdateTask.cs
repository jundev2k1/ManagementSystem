// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Commands.UpdateTask;
using Application.Features.Tasks.Dtos;

namespace WebAPI.Endpoints;

public sealed class UpdateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/tasks/{taskId:guid}", async (
			[FromRoute] Guid taskId,
			TaskInfoRequestDto request,
			[FromServices] ISender sender,
			[FromServices] ILogger<UpdateTask> logger,
			CancellationToken cancellationToken) =>
		{
			if (request.TaskId != taskId)
			{
				logger.LogError("Task ID in the request does not match the Task ID in the URL.");
				throw new BadRequestException("Task ID mismatch.");
			}

			var command = new UpdateTaskCommand(request.Adapt<TaskInfo>());
			var result = await sender.Send(command, cancellationToken);

			return ApiResponse<object?>.Ok();
		}).RequireAuthorization();
	}
}
