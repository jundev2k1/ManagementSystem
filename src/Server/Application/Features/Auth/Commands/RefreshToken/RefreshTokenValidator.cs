// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;

namespace Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
	public RefreshTokenValidator()
	{
		RuleFor(x => x.RefreshToken)
			.NotEmpty().WithMessage("Refresh token is required.");
	}
}
