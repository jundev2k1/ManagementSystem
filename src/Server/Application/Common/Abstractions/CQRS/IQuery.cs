// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
	where TResponse : notnull
{
}
