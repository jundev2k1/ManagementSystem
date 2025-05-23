// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Features.Tasks.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid TaskId) : IQuery<TaskInfo>;
