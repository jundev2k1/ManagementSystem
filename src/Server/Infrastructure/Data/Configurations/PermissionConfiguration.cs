// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<Permission> builder)
	{
		builder.ToTable("permission");
		builder.HasKey(u => u.PermissionId);
		builder.Property(u => u.PermissionId)
			.IsRequired();
		builder.Property(u => u.PermissionName)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.DisplayName)
			.IsRequired()
			.HasMaxLength(128)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Description)
			.IsRequired()
			.HasMaxLength(4000)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Category)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Action)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.ValidFlg)
			.IsRequired()
			.HasDefaultValue(true);
		builder.Property(u => u.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(u => u.CreatedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.LastModifiedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(u => u.LastModifiedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
	}

	private void ConfigureIndexes(EntityTypeBuilder<Permission> builder)
	{
		builder.HasIndex(u => u.PermissionName)
			.IsUnique()
			.HasDatabaseName("ix_permission_permission_name");
		builder.HasIndex(u => u.Category)
			.HasDatabaseName("ix_permission_category");
		builder.HasIndex(u => u.Action)
			.HasDatabaseName("ix_permission_action");
		builder.HasIndex(u => u.ValidFlg)
			.HasDatabaseName("ix_permission_validflg");
	}
}
