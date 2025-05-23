// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Endpoints;

public sealed class CreateTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/tasks", async (TaskInfo request, CancellationToken cancellationToken) =>
		{
			return await Task.FromResult(JsonSerializer.Serialize(request));
		});
	}
}
