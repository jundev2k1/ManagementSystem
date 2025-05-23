// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.Extensions;

public sealed class DatabaseInitializer(ApplicationDbContext dbContext, ILogger<DatabaseInitializer> logger)
{
	public async Task InitialiseDatabaseAsync()
	{
		await dbContext.Database.MigrateAsync();

		await RunTaskMigration();
	}

	private async Task RunTaskMigration()
	{
		foreach (var script in InitialData.TaskSearchVectorScripts)
		{
			await dbContext.Database.ExecuteSqlRawAsync(script);
		}
	}
}
