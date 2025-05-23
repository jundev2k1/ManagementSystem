// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace WebAPI;

public static class DependencyInjection
{
	public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOpenApi();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddCarter();

		return services;
	}

	public static WebApplication UseWebApi(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.MapCarter();

		return app;
	}
}
