// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Endpoints;

public sealed class DeleteTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/tasks/{taskId:guid}", async (
			[FromRoute] string taskId,
			CancellationToken cancellationToken) =>
		{
			return await Task.FromResult(JsonSerializer.Serialize(taskId));
		});
	}
}
