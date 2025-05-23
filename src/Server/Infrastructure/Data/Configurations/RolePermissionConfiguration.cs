// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<RolePermission> builder)
	{
		builder.ToTable("role_permission");
		builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
		builder.Property(rp => rp.RoleId)
			.IsRequired();
		builder.Property(rp => rp.PermissionId)
			.IsRequired();
		builder.Property(rp => rp.GrantedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(rp => rp.GrantedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(rp => rp.CreatedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(rp => rp.CreatedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(rp => rp.LastModifiedAt)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(rp => rp.LastModifiedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
	}

	private void ConfigureIndexes(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasIndex(ur => new { ur.RoleId, ur.PermissionId })
			.HasDatabaseName("ix_rolepermission_roleid_permissionid"); ;
	}
}
