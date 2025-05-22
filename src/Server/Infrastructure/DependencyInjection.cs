// Copyright (c) 2025 - Jun Dev. All rights reserved

using Infrastructure.Data;
using Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbConnection(configuration);
		services.AddScoped<DatabaseInitializer>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}

	private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			options
				.UseNpgsql(connectionString, o => o.MigrationsHistoryTable("__ef_migrations_history"))
				.UseSnakeCaseNamingConvention();
		});

		return services;
	}
}
