// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public sealed class TaskConfiguration : IEntityTypeConfiguration<TaskInfo>
{
	public void Configure(EntityTypeBuilder<TaskInfo> builder)
	{
		ConfigureKeys(builder);
		ConfigureIndexes(builder);
	}

	private void ConfigureKeys(EntityTypeBuilder<TaskInfo> builder)
	{
		builder.ToTable("task");
		builder.HasKey(t => t.TaskId);
		builder.Property(t => t.TaskId)
			.IsRequired();
		builder.Property(t => t.Title)
			.IsRequired()
			.HasMaxLength(128)
			.HasDefaultValue(string.Empty);
		builder.Property(t => t.Description)
			.IsRequired()
			.HasMaxLength(4000)
			.HasDefaultValue(string.Empty);
		builder.Property(t => t.Status)
			.IsRequired()
			.HasConversion<int>()
			.HasDefaultValue(TaskStatusEnum.New);
		builder.Property(t => t.Progress)
			.IsRequired()
			.HasAnnotation("MaxValue", 100)
			.HasDefaultValue(0);
		builder.Property(t => t.StartDate)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v);
		builder.Property(t => t.DueDate)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v);
		builder.Property(t => t.ActualStartDate)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v);
		builder.Property(t => t.ActualEndDate)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v);
		builder.Property(t => t.Priority)
			.IsRequired()
			.HasConversion<int>()
			.HasDefaultValue(TaskPriorityEnum.None);
		builder.Property(t => t.AssignedTo);
		builder.Property(t => t.AssignedBy);
		builder.Property(t => t.Note)
			.IsRequired()
			.HasMaxLength(4000)
			.HasDefaultValue(string.Empty);
		builder.Property(t => t.CreatedAt)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(t => t.CreatedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
		builder.Property(t => t.LastModifiedAt)
			.HasConversion(v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v, v => v)
			.IsRequired()
			.HasDefaultValueSql("CURRENT_TIMESTAMP");
		builder.Property(t => t.LastModifiedBy)
			.IsRequired()
			.HasMaxLength(60)
			.HasDefaultValue(string.Empty);
	}

	private void ConfigureIndexes(EntityTypeBuilder<TaskInfo> builder)
	{
		builder.HasIndex(t => t.Status)
			.HasDatabaseName("ix_task_status");
		builder.HasIndex(t => t.Priority)
			.HasDatabaseName("ix_task_priority");
		builder.HasIndex(t => t.DueDate)
			.HasDatabaseName("ix_task_duedate");
		builder.HasIndex(t => t.AssignedTo)
			.HasDatabaseName("ix_task_assignedto");
		builder.HasIndex(t => t.AssignedBy)
			.HasDatabaseName("ix_task_assignedby");
		builder.HasIndex(t => t.CreatedAt)
			.HasDatabaseName("ix_task_createdat");

		// Composite index
		builder.HasIndex(t => new { t.Status, t.DueDate })
			.HasDatabaseName("ix_task_status_duedate");
	}
}
