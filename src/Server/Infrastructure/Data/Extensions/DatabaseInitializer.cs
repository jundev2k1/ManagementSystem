// Copyright (c) 2025 - Jun Dev. All rights reserved

namespace Infrastructure.Data.Extensions;

public sealed class DatabaseInitializer(ApplicationDbContext dbContext)
{
	public async Task InitialiseDatabaseAsync()
	{
		await dbContext.Database.MigrateAsync();
	}
}
