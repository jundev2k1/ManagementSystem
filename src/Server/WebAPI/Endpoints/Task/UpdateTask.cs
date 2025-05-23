// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Endpoints;

public sealed class UpdateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/tasks/{taskId:guid}", async (
			[FromRoute] string taskId,
			[FromForm]TaskInfo request,
			CancellationToken cancellationToken) =>
		{
			return await Task.FromResult(JsonSerializer.Serialize(request));
		});
	}
}
