﻿// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Mapping;
using Application.Features.Tasks.Queries.GetByCriteria;

namespace WebAPI.Endpoints;

public sealed class GetTaskByCriteria : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/tasks", async (
			[AsParameters] SearchCriteriaRequest parameters,
			[FromServices] ISender sender,
			[FromServices] ILogger<GetTaskByCriteria> logger,
			CancellationToken cancellationToken) =>
		{
			var filters = QueryConverter.ToFilters(parameters.Filters, parameters.Keyword);
			var sorts = QueryConverter.ToSorts(parameters.Sorts);
			var query = new GetByCriteriaQuery(
				Filters: filters,
				Sorts: sorts,
				PageNumber: parameters.PageNumber,
				PageSize: parameters.PageSize);

			var result = await sender.Send(query, cancellationToken);
			var response = ApiResponse<PaginationResult<TaskInfo>>.Ok(result);
			logger.LogInformation("Response: " + JsonSerializer.Serialize(response));

			return response;
		}).RequireAuthorization();
	}
}
