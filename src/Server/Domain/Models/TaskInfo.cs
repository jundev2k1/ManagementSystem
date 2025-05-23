// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Abstractions;
using Domain.Enums;

namespace Domain.Models;

public sealed class TaskInfo : Entity
{
	public Guid TaskId { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public TaskStatusEnum Status { get; set; } = TaskStatusEnum.New;
	public int Progress { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? DueDate { get; set; }
	public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.None;
	public Guid? AssignedTo { get; set; }
	public Guid? AssignedBy { get; set; }
	public string Note { get; set; } = string.Empty;
}
