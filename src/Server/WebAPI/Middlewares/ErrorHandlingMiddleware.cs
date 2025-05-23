// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Net;

namespace WebAPI.Middlewares;

public sealed class ErrorHandlingMiddleware
{
	private readonly RequestDelegate _next;
	public ErrorHandlingMiddleware(RequestDelegate next)
	{
		_next = next;
	}
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
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
			UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
			ForbiddenAccessException => (int)HttpStatusCode.Forbidden, // 403
			NotFoundException => (int)HttpStatusCode.NotFound, // 404
			InvalidOperationException => (int)HttpStatusCode.InternalServerError, // 500
			_ => (int)HttpStatusCode.InternalServerError // 500
		};

		var result = ApiResponse<object>.Fail(exception.Message);
		return context.Response.WriteAsync(JsonSerializer.Serialize(result));
	}
}
