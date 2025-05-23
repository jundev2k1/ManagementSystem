// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data;

public sealed class UnitOfWork : IUnitOfWork
{
	private ApplicationDbContext _dbContext;
	private IDbContextTransaction? _transaction;
	public UnitOfWork(
		ApplicationDbContext dbContext,
		ITaskRepository taskRepository)
	{
		_dbContext = dbContext;
		this.Tasks = taskRepository;
	}


	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
	{
		_transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
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
		return await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		_dbContext.Dispose();
	}

	public ITaskRepository Tasks { get; }
}
