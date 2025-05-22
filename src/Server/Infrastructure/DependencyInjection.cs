// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Data;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}
