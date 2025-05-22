// Copyright (c) 2025 - Jun Dev. All rights reserved

using Application.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Data;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly IDbConnection _connection;
	private IDbTransaction? _transaction;

	public UnitOfWork(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("DefaultConnection");
		_connection = new NpgsqlConnection(connectionString);
		_connection.Open();
	}

	public void BeginTransaction()
	{
		if (_transaction == null)
			_transaction = _connection.BeginTransaction();
	}

	public void Commit()
	{
		if (_transaction != null)
		{
			_transaction.Commit();
			_transaction.Dispose();
			_transaction = null;
		}
	}

	public void Rollback()
	{
		if (_transaction != null)
		{
			_transaction.Rollback();
			_transaction.Dispose();
			_transaction = null;
		}
	}

	public void Dispose()
	{
		_transaction?.Dispose();
		_connection?.Dispose();
	}

	public IDbConnection Connection => _connection;
	public IDbTransaction? Transaction => _transaction;
}
