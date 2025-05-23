// Copyright (c) 2025 - Jun Dev. All rights reserved

using System.Reflection;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}

	// Entities related to user authentication and authorization
	public DbSet<User> Users { get; set; }
	public DbSet<Role> Roles { get; set; }
}
