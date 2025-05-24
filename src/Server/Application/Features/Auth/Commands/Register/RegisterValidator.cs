// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand>
{
	public RegisterValidator()
	{
		RuleFor(x => x.UserInput.UserName)
			.NotEmpty().WithMessage("Username is required.")
			.MinimumLength(6).WithMessage("Username must be at least 6 characters long.");

		RuleFor(x => x.UserInput.Email)
			.NotEmpty().WithMessage("Email is required.")
			.EmailAddress().WithMessage("Invalid email format.");

		RuleFor(x => x.UserInput.Password)
			.NotEmpty().WithMessage("Password is required.")
			.MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

		RuleFor(x => x.UserInput.FirstName)
			.NotEmpty().WithMessage("First name is required.")
			.MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

		RuleFor(x => x.UserInput.LastName)
			.NotEmpty().WithMessage("Last name is required.")
			.MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

		RuleFor(x => x.UserInput.PhoneNumber)
			.NotEmpty().WithMessage("Phone number is required.")
			.MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.")
			.Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("Invalid phone number format.");

		RuleFor(x => x.UserInput.Address)
			.NotEmpty().WithMessage("Address is required.")
			.MaximumLength(255).WithMessage("Address cannot exceed 255 characters.");
	}
}
