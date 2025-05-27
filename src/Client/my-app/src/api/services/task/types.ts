// Copyright (c) 2025 - Jun Dev. All rights reserved

export interface TaskModel {
  taskId: string;
  title: string;
  description: string;
  status: 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7;
  progress: number;
  startDate: string;
  dueDate: string;
  priority: 0 | 1 | 2 | 3 | 4;
  assignedTo: string;
  assignedBy: string;
  note: string;
	createdAt: string;
	createdBy: string;
	lastModifiedAt: string;
	lastModifiedBy: string;
}
