// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Application.Data;

public interface IUnitOfWork : IDisposable
{
	Task BeginTransactionAsync(CancellationToken cancellationToken = default);
	Task CommitAsync(CancellationToken cancellationToken = default);
	Task RollbackAsync(CancellationToken cancellationToken = default);

	Task<int> SaveAsync(CancellationToken cancellationToken = default);
}
