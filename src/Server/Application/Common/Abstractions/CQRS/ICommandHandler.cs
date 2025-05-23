// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.CQRS;

public interface ICommandHandler<in TCommand>
	: IRequestHandler<TCommand, Unit>
	where TCommand : ICommand<Unit>
{
}

public interface ICommandHandler<in TCommand, TResponse>
	: IRequestHandler<TCommand, TResponse>
	where TCommand : ICommand<TResponse>
	where TResponse : notnull
{
}
