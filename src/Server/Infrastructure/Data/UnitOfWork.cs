// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
	private IDbContextTransaction? _transaction;

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		_transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
	}

	public async Task CommitAsync(CancellationToken cancellationToken = default)
	{
		if (_transaction is null) return;

		await _transaction.CommitAsync(cancellationToken);
		await _transaction.DisposeAsync();
	}

	public async Task RollbackAsync(CancellationToken cancellationToken = default)
	{
		if (_transaction is null) return;

		await _transaction.RollbackAsync(cancellationToken);
		await _transaction.DisposeAsync();
	}

	public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
	{
		return await dbContext.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		dbContext.Dispose();
	}
}
