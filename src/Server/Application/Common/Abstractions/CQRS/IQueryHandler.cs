// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.CQRS;

public interface IQueryHandler<in TQuery, TResponse>
	: IRequestHandler<TQuery, TResponse>
	where TQuery : IQuery<TResponse>
	where TResponse : notnull
{
}
