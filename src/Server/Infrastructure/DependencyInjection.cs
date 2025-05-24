// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Data.Extensions;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbConnection(configuration);
		services.AddRepositories();
		services.AddJwtAuthentication(configuration);
		services.AddSecurity();

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<DatabaseInitializer>();
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

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
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(option =>
			{
				option.RequireHttpsMetadata = false;
				option.SaveToken = true;
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings.Issuer,
					ValidAudience = jwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(jwtSettings.SecretKey!))
				};
				option.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						context.Response.StatusCode = 401; // Unauthorized
						return Task.CompletedTask;
					},
					OnChallenge = context =>
					{
						context.Response.StatusCode = 401; // Unauthorized
						return Task.CompletedTask;
					}
				};
			});
		services.AddAuthorization();

		return services;
	}

	private static IServiceCollection AddSecurity(this IServiceCollection services)
	{
		services.AddScoped<IPasswordHasher, PasswordHasherService>();
		return services;
	}
}
