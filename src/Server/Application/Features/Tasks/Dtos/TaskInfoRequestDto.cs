// Copyright (c) 2025 - Jun Dev. All rights reserved

using Domain.Enums;

namespace Application.Features.Tasks.Dtos;

public sealed class TaskInfoRequestDto
{
	public Guid TaskId { get; set; } = Guid.Empty;
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public TaskStatusEnum Status { get; set; } = TaskStatusEnum.New;
	public int Progress { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? DueDate { get; set; }
	public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.None;
	public Guid? AssignedTo { get; set; }
	public Guid? AssignedBy { get; set; }
	public string? Note { get; set; } = string.Empty;
}
