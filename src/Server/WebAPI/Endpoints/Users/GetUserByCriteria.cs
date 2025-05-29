// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Mapping;
using Application.Features.Users.Dtos;
using Application.Features.Users.Queries.GetByCriteria;

namespace WebAPI.Endpoints;

public sealed class GetUserByCriteria : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/users", async (
			[AsParameters] SearchCriteriaRequest parameters,
			[FromServices] ISender sender,
			[FromServices] ILogger<GetUserByCriteria> logger,
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
			var searchResult = new PaginationResult<UserDto>(
				items: result.Items.Adapt<IEnumerable<UserDto>>(),
				totalItems: result.TotalItems,
				totalPages: result.TotalPages,
				pageIndex: result.PageNumber,
				pageSize: result.PageSize);
			var response = ApiResponse<PaginationResult<UserDto>>.Ok(searchResult);

			logger.LogInformation("Response: " + JsonSerializer.Serialize(response));
			return response;
		}).RequireAuthorization();
	}
}
