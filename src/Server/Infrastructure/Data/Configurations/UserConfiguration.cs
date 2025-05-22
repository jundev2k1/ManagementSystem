// Copyright (c) 2025 - Jun Dev. All rights reserved

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("user");
		builder.HasKey(u => u.UserId);
		builder.Property(u => u.UserId)
			.IsRequired();
		builder.Property(u => u.UserName)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Email)
			.IsRequired()
			.HasMaxLength(128)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.PasswordHash)
			.IsRequired()
			.HasMaxLength(255)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Avatar)
			.IsRequired()
			.HasMaxLength(255)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.FirstName)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.LastName)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.PhoneNumber)
			.IsRequired()
			.HasMaxLength(20)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Address)
			.HasMaxLength(255)
			.IsRequired()
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

	private void ConfigureIndexes(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(u => u.UserName)
			.IsUnique()
			.HasDatabaseName("ix_user_username");
		builder.HasIndex(u => u.Email)
			.IsUnique()
			.HasDatabaseName("ix_user_email");
	}
}
