// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand(TaskInfo Task) : ICommand;
