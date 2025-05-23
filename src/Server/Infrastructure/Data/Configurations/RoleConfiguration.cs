// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable("role");
		builder.HasKey(u => u.RoleId);
		builder.Property(u => u.RoleId)
			.IsRequired();
		builder.Property(u => u.RoleName)
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
		builder.Property(u => u.ValidFlg)
			.IsRequired()
			.HasDefaultValue(true);
		builder.Property(u => u.IsSystem)
			.IsRequired()
			.HasDefaultValue(false);
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

	private void ConfigureIndexes(EntityTypeBuilder<Role> builder)
	{
	}
}
