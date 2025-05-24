// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Queries.GetTaskById;

namespace WebAPI.Endpoints;

public sealed class GetTaskById : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/task/{taskId:guid}", async (
			[FromRoute] Guid taskId,
			[FromServices] ISender sender,
			[FromServices] ILogger<GetTaskById> logger,
			CancellationToken cancellationToken) =>
		{
			var result = await sender.Send(new GetTaskByIdQuery(taskId), cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<TaskInfo>.Ok(result);
		}).RequireAuthorization();
	}
}
