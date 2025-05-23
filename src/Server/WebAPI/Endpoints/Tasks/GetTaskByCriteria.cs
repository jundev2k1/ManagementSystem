// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
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
			var query = parameters.Adapt<GetByCriteriaQuery>();
			var result = await sender.Send(query, cancellationToken);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(result));

			return ApiResponse<PaginationResult<TaskInfo>>.Ok(result);
		});
	}
}
