// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI.Endpoints;

public sealed class GetTaskByCriteria : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/task", async (CancellationToken cancellationToken) =>
		{
			return await Task.FromResult("Call success");
		});
	}
}
