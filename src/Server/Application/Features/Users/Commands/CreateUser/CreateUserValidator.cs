// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator()
	{
		RuleFor(x => x.User.UserName)
			.NotEmpty().WithMessage("Username is required.")
			.MinimumLength(6).WithMessage("Username must be at least 6 characters long.");

		RuleFor(x => x.User.Email)
			.NotEmpty().WithMessage("Email is required.")
			.MaximumLength(128).WithMessage("First name cannot exceed 128 characters.")
			.EmailAddress().WithMessage("Invalid email format.");

		RuleFor(x => x.User.Password)
			.NotEmpty().WithMessage("Password is required.")
			.MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

		RuleFor(x => x.User.FirstName)
			.NotEmpty().WithMessage("First name is required.")
			.MaximumLength(60).WithMessage("First name cannot exceed 60 characters.");

		RuleFor(x => x.User.LastName)
			.NotEmpty().WithMessage("Last name is required.")
			.MaximumLength(60).WithMessage("Last name cannot exceed 60 characters.");

		RuleFor(x => x.User.PhoneNumber)
			.NotEmpty().WithMessage("Phone number is required.")
			.MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
			.Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("Invalid phone number format.");

		RuleFor(x => x.User.Address)
			.NotEmpty().WithMessage("Address is required.")
			.MaximumLength(255).WithMessage("Address cannot exceed 255 characters.");
	}
}
