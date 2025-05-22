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
		builder.ToTable("users");
		builder.HasKey(u => u.UserId);
		builder.Property(u => u.UserId)
			.HasColumnName("user_id")
			.IsRequired();
		builder.Property(u => u.UserName)
			.HasColumnName("username")
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Email)
			.HasColumnName("email")
			.IsRequired()
			.HasMaxLength(128)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.PasswordHash)
			.HasColumnName("password_hash")
			.IsRequired()
			.HasMaxLength(255)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Avatar)
			.HasColumnName("avatar")
			.IsRequired()
			.HasMaxLength(255)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.FirstName)
			.HasColumnName("first_name")
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.LastName)
			.HasColumnName("last_name")
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.PhoneNumber)
			.HasColumnName("phone_number")
			.IsRequired()
			.HasMaxLength(20)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.Address)
			.HasColumnName("address")
			.HasMaxLength(255)
			.IsRequired()
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.ValidFlg)
			.IsRequired()
			.HasColumnName("valid_flg")
			.HasDefaultValue(true);
		builder.Property(u => u.CreatedAt)
			.HasColumnName("created_at")
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(u => u.CreatedBy)
			.IsRequired()
			.HasColumnName("created_by")
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(u => u.LastModifiedAt)
			.IsRequired()
			.HasColumnName("last_modified_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(u => u.LastModifiedBy)
			.IsRequired()
			.HasColumnName("last_modified_by")
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
