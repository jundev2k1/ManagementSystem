// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Common.Abstractions.CQRS;

public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
