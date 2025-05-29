// Copyright (c) 2025 - Jun Dev. All rights reserved

import * as Yup from "yup";
import type { TaskModel } from "../../../api/services";

export const intialTaskValues: TaskModel = {
  taskId: "",
  title: "",
  description: "",
  status: 0,
  progress: 0,
  startDate: null,
  dueDate: null,
  actualStartDate: null,
  actualEndDate: null,
  priority: 0,
  assignedTo: "",
  assignedBy: "",
  note: "",
  createdAt: "",
  createdBy: "",
  lastModifiedAt: "",
  lastModifiedBy: "",
};

export const validateSchema = Yup.object({
  title: Yup.string()
    .required("Title is required")
    .max(200, "Title must be at most 200 characters"),
  description: Yup.string().max(
    4000,
    "Description must be at most 4000 characters",
  ),
  status: Yup.number()
    .required("Status is required")
    .min(0, "Invalid status value")
    .max(7, "Invalid status value"),
  progress: Yup.number()
    .required("Progress is required")
    .min(0, "Progress must be at least 0")
    .max(100, "Progress cannot exceed 100"),
  startDate: Yup.date()
    .nullable()
    .typeError("Start date must be a valid date"),
  dueDate: Yup.date()
    .nullable()
    .min(Yup.ref("startDate"), "Due date cannot be before start date")
    .typeError("Due date must be a valid date"),
  actualStartDate: Yup.date()
    .nullable()
    .typeError("Actual start date must be a valid date"),
  actualEndDate: Yup.date()
    .nullable()
    .min(Yup.ref("actualEndDate"), "Actual end date cannot be before start date")
    .typeError("Actual end date must be a valid date"),
  priority: Yup.number()
    .required("Priority is required")
    .min(0, "Invalid priority")
    .max(4, "Priority cannot be greater than 4"),
  assignedTo: Yup.string().nullable(),
  assignedBy: Yup.string().nullable(),
  note: Yup.string().max(4000, "Note must be at most 4000 characters"),
});

export const createTaskRequest = (values: TaskModel, isCreated = true) : TaskModel => ({
  ...values,
  taskId: !isCreated ? values.taskId : undefined,
  status: Number(`${values.status}`) as 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7,
  startDate: values.startDate || undefined,
  dueDate: values.dueDate || undefined,
  actualStartDate: values.actualStartDate || undefined,
  actualEndDate: values.actualEndDate || undefined,
  assignedBy: values.assignedBy || undefined,
  assignedTo: values.assignedTo || undefined,
})
