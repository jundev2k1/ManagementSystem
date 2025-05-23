// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException() : base("The requested resource was not found.")
	{
	}

	public NotFoundException(string message) : base(message)
	{
	}

	public NotFoundException(string message, Exception exp) : base(message, exp)
	{
	}

	public NotFoundException(string name, object key)
		: base($"Entity \"{name}\" ({key}) was not found.")
	{
	}
}
