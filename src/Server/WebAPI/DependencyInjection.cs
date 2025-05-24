// Copyright (c) 2025 - Jun Dev. All rights reserved

using WebAPI.Middlewares;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

namespace WebAPI;

public static class DependencyInjection
{
	public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOpenApi();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerUI();

		services.AddCarter();

		return services;
	}

	private static IServiceCollection AddSwaggerUI(this IServiceCollection services)
	{
		services.AddSwaggerGen(config =>
		{
			config.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management API", Version = "v1" });
			config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = Microsoft.OpenApi.Models.ParameterLocation.Header,
				Description = "Enter JWT token with prefix 'Bearer {Token}'"
			});

			config.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
		});

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
		app.UseMiddleware();
		app.UseAuthentication();
		app.UseAuthorization();

		return app;
	}

	public static WebApplication UseMiddleware(this WebApplication app)
	{
		app.UseMiddleware<ErrorHandlingMiddleware>();
		app.UseMiddleware<RequestLoggingMiddleware>();

		return app;
	}
}
