// Copyright (c) 2025 - Jun Dev. All rights reservedssss

namespace Application.Common.Exceptions;

public class BadRequestException : Exception
{
	public BadRequestException() : base("The request is invalid.")
	{
	}

	public BadRequestException(string message) : base(message)
	{
	}

	public BadRequestException(string message, Exception exp) : base(message, exp)
	{
	}
}
