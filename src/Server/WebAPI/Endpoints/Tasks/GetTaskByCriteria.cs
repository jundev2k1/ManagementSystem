// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Features.Tasks.Queries.GetByCriteria;
using WebAPI.Dtos;

namespace WebAPI.Endpoints;

public sealed class GetTaskByCriteria : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/task", async (
			[AsParameters] TaskSearchCriteriaRequest parameters,
			[FromServices] ISender sender,
			[FromServices] ILogger<GetTaskByCriteria> logger,
			CancellationToken cancellationToken) =>
		{
			logger.LogInformation("Endpoint => Get task by criteria");

			var query = parameters.Adapt<GetByCriteriaQuery>();
			logger.LogInformation("Request: " + JsonSerializer.Serialize(query));

			var result = await sender.Send(query, cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return result;
		});
	}
}
