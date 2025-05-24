// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;

namespace Application.Common.Interfaces.Repositories;

public interface ITaskRepository
{
	Task<PaginationResult<TaskInfo>> GetTasksByCriteriaAsync(
		Func<IQueryable<TaskInfo>, IQueryable<TaskInfo>>? queryBuilder = null,
		int page = 1,
		int pageSize = 20,
		CancellationToken cancellationToken = default);

	Task<TaskInfo?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<Guid> CreateNewTaskAsync(TaskInfo task, CancellationToken cancellationToken = default);

	Task UpdateTaskAsync(TaskInfo task, CancellationToken cancellationToken = default);

	Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
