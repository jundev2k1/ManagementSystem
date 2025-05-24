// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces.Repositories;

namespace Application.Data;

public interface IUnitOfWork : IDisposable
{
	Task BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitAsync(CancellationToken cancellationToken = default);
	Task RollbackAsync(CancellationToken cancellationToken = default);

	Task<int> SaveAsync(CancellationToken cancellationToken = default);

	ITaskRepository Tasks { get; }
	IUserRepository Users { get; }
}
