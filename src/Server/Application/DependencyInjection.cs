// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Behaviors;
using Application.Common.Filters;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			config.AddOpenBehavior(typeof(ValidationBehavior<,>));
			config.AddOpenBehavior(typeof(LoggingBehavior<,>));
		});
		services.AddCommonServices();

		return services;
	}

	private static IServiceCollection AddCommonServices(this IServiceCollection services)
	{
		services.AddScoped(typeof(ICriteriaBuilder<>), typeof(CriteriaBuilder<>));
		return services;
	}
}
