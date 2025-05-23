// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Extensions;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbConnection(configuration);
		services.AddRepositories();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<DatabaseInitializer>();

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

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ITaskRepository, TaskRepository>();

		return services;
	}
}
