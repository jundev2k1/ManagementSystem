// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("user_role");
		builder.HasKey(ur => new { ur.UserId, ur.RoleId });
		builder.Property(ur => ur.UserId)
			.IsRequired();
		builder.Property(ur => ur.RoleId)
			.IsRequired();
		builder.Property(ur => ur.AssignedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(ur => ur.AssignedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(ur => ur.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(ur => ur.CreatedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(ur => ur.LastModifiedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(ur => ur.LastModifiedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
	}

	private void ConfigureIndexes(EntityTypeBuilder<UserRole> builder)
	{
		builder.HasIndex(ur => new { ur.UserId, ur.RoleId })
			.HasDatabaseName("ix_userrole_userid_roleid"); ;
	}
}
