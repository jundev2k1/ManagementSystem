// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Net;

namespace WebAPI.Middlewares;

public sealed class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHandlingMiddleware> _logger;

	public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);

			if (context.Response.StatusCode == 401)
				throw new UnauthorizedAccessException("Unauthorized request");

			if (context.Response.StatusCode == 403)
				throw new ForbiddenAccessException();
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);

			if (ex is ValidationException exception)
			{
				var errorMessage = string.Join(", ", exception.Errors.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}"));
				_logger.LogError(ex, "Validation error occurred: {Message}", errorMessage);
			}
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = exception switch
		{
			BadRequestException => (int)HttpStatusCode.BadRequest, // 400
			ArgumentNullException => (int)HttpStatusCode.BadRequest, // 400
			ArgumentOutOfRangeException => (int)HttpStatusCode.BadRequest, // 400
			ValidationException => (int)HttpStatusCode.BadRequest, // 400
			UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
			ForbiddenAccessException => (int)HttpStatusCode.Forbidden, // 403
			NotFoundException => (int)HttpStatusCode.NotFound, // 404
			InvalidOperationException => (int)HttpStatusCode.InternalServerError, // 500
			NotSupportedException => (int)HttpStatusCode.NotImplemented, // 501
			_ => (int)HttpStatusCode.InternalServerError // 500
		};

		var result = ApiResponse<object>.Fail(exception.Message);
		return context.Response.WriteAsync(JsonSerializer.Serialize(result));
	}
}
