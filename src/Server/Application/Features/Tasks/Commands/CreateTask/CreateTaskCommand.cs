// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand(TaskInfo Task) : ICommand<Guid>;
