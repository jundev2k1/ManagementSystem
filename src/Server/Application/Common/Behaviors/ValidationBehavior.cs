// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
	IEnumerable<IValidator<TRequest>> validators)
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : ICommand<TResponse>
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var context = new ValidationContext<TRequest>(request);

		var failures = await Task.WhenAll(
			validators.Select(v => v.ValidateAsync(context, cancellationToken)));
		var validationFailures = failures
			.SelectMany(r => r.Errors)
			.Where(f => f is not null)
			.ToList();
		if (validationFailures.Any())
			throw new ValidationException(validationFailures);

		return await next();
	}
}
