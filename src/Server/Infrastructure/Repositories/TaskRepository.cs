// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Abstractions.Pagination;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public sealed class TaskRepository(ApplicationDbContext dbContext) : ITaskRepository
{
	public async Task<PaginationResult<TaskInfo>> GetTasksByCriteriaAsync(
		Func<IQueryable<TaskInfo>, IQueryable<TaskInfo>>? queryBuilder = null,
		int page = 1,
		int pageSize = 20,
		CancellationToken cancellationToken = default)
	{
		var query = dbContext.Tasks.AsQueryable();

		if (queryBuilder is not null)
			query = queryBuilder(query);

		var totalCount = await query.CountAsync(cancellationToken);
		var items = await query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

		return new PaginationResult<TaskInfo>(
			items: items,
			totalItems: totalCount,
			totalPages: (int)Math.Ceiling((decimal)totalCount / pageSize),
			pageIndex: page,
			pageSize: pageSize);
	}
	public async Task<TaskInfo?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Tasks
			.AsNoTracking()
			.FirstOrDefaultAsync(t => t.TaskId == id, cancellationToken);
		return targetTask;
	}


	public async Task<Guid> CreateNewTaskAsync(TaskInfo task, CancellationToken cancellationToken = default)
	{
		await dbContext.Tasks.AddAsync(task, cancellationToken);
		return task.TaskId;
	}

	public async Task UpdateTaskAsync(TaskInfo task, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Tasks
			.FirstOrDefaultAsync(t => t.TaskId == task.TaskId, cancellationToken);
		if (targetTask is null) throw new NotFoundException(name: "Task", key: task.TaskId);

		// Set the properties of the target task to the values from the source task
		targetTask.Title = task.Title;
		targetTask.Description = task.Description;
		targetTask.Status = task.Status;
		targetTask.Progress = task.Progress;
		targetTask.StartDate = task.StartDate;
		targetTask.DueDate = task.DueDate;
		targetTask.Priority = task.Priority;
		targetTask.AssignedTo = task.AssignedTo;
		targetTask.AssignedBy = task.AssignedBy;
		targetTask.Note = task.Note;
		targetTask.LastModifiedAt = DateTime.UtcNow;
		targetTask.LastModifiedBy = task.LastModifiedBy;
	}

	public async Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var targetTask = await dbContext.Tasks.AsNoTracking()
			.FirstOrDefaultAsync(t => t.TaskId == id, cancellationToken);
		if (targetTask is null) throw new NotFoundException(name: "Task", key: id);

		dbContext.Tasks.Remove(targetTask);
	}
}
