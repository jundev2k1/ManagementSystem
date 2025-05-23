// Copyright (c) 2025 - Jun Dev. All rights reserved

using FluentValidation;

namespace Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(TaskInfo Task) : ICommand<Guid>;
