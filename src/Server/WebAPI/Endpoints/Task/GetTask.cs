// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Endpoints;

public sealed class GetTask : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/task/{taskId:guid}", async ([FromRoute] Guid taskId, CancellationToken cancellationToken) =>
		{
			return await Task.FromResult($"Call success: {taskId}");
		});
	}
}
