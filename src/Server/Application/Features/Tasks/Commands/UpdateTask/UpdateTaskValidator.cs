// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;

namespace Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
	public UpdateTaskCommandValidator()
	{
		RuleFor(t => t.Task.TaskId)
			.NotEmpty().WithMessage("TaskId is required.")
			.NotEqual(Guid.Empty).WithMessage("TaskId cannot be empty.");

		RuleFor(t => t.Task.Description)
			.NotEmpty().WithMessage("Description is required.")
			.MaximumLength(4000).WithMessage("Description cannot exceed 4000 characters.");

		RuleFor(t => t.Task.Status)
			.IsInEnum().WithMessage("Invalid status.");

		RuleFor(t => t.Task.Progress)
			.InclusiveBetween(0, 100).WithMessage("Progress must be between 0 and 100.");

		RuleFor(t => t.Task.Priority)
			.IsInEnum().WithMessage("Invalid priority.");

		RuleFor(t => t.Task.DueDate)
			.GreaterThan(t => t.Task.StartDate).WithMessage("DueDate must be greater than StartDate");

		RuleFor(t => t.Task.ActualEndDate)
			.GreaterThan(t => t.Task.ActualStartDate).WithMessage("ActualEndDate must be greater than ActualStartDate");

		RuleFor(t => t.Task.AssignedBy)
			.Must(id => id == null || id != Guid.Empty).WithMessage("AssignedBy cannot be empty if provided.");

		RuleFor(t => t.Task.AssignedTo)
			.Must(id => id == null || id != Guid.Empty).WithMessage("AssignedTo cannot be empty if provided.");

		RuleFor(t => t.Task.Note)
			.MaximumLength(4000).WithMessage("Note cannot exceed 4000 characters.");
	}
}
