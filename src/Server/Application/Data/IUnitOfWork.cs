// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Data;
using System.Data.Common;

namespace Application.Data;

public interface IUnitOfWork
{
	IDbConnection Connection { get; }
	IDbTransaction? Transaction { get; }

	void BeginTransaction();
	void Commit();
	void Rollback();
	void Dispose();
}
