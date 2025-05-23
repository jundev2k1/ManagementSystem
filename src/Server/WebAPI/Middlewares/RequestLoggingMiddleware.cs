// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Text;

namespace WebAPI.Middlewares;

public sealed class RequestLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<RequestLoggingMiddleware> _logger;
	public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		context.Request.EnableBuffering();
		var requestBody = await ReadBodyRequest(context);

		_logger.LogInformation(
			"Request: IP: {IP} | {Method} {Path} {Query} {Body}",
			context.Connection.RemoteIpAddress,
			context.Request.Method,
			context.Request.Path,
			context.Request.QueryString,
			requestBody);

		await _next(context);
	}

	private async Task<string> ReadBodyRequest(HttpContext context)
	{

		if ((context.Request.ContentLength is null) || (context.Request.ContentLength == 0))
			return string.Empty;

		using var reader = new StreamReader(
			context.Request.Body,
			encoding: Encoding.UTF8,
			detectEncodingFromByteOrderMarks: false,
			bufferSize: 1024,
			leaveOpen: true);

		var requestBody = await reader.ReadToEndAsync();
		context.Request.Body.Position = 0;

		return requestBody;
	}
}
