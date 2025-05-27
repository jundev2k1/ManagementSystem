// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Auth;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.Services;
using Infrastructure.Auth;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Data.Caching;
using Infrastructure.Data.Extensions;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Services;
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
		services
			.AddDbConnection(configuration)
			.AddRedisCaching(configuration)
			.AddJwtAuthentication(configuration)
			.AddRepositories()
			.AddServices()
			.AddSecurity();

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

	private static IServiceCollection AddRedisCaching(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddStackExchangeRedisCache(option =>
		{
			option.Configuration = configuration.GetConnectionString("Redis");
			option.InstanceName = "ManagementSystem:";
		});

		services.AddSingleton<IRedisCache, RedisCache>();
		services.AddScoped<IRefreshTokenCache, RefreshTokenCache>();

		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ITaskRepository, TaskRepository>();
		services.AddScoped<IUserRepository, UserRepository>();

		return services;
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		services.AddScoped<ICurrentUser, CurrentUser>();
		services.AddScoped<IAuthService, AuthService>();

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
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(jwtSettings.SecretKey!))
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
