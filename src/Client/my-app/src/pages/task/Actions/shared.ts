// Copyright (c) 2025 - Jun Dev. All rights reserved

import * as Yup from "yup";
import type { TaskModel } from "../../../api/services/task";
import { formatDate, getToday, getTomorrow } from "../../../common/utils/datetime";

export const intialTaskValues: TaskModel = {
  taskId: "",
  title: "",
  description: "",
  status: 0,
  progress: 0,
  startDate: formatDate(getToday(), "yyyy-MM-dd"),
  dueDate: formatDate(getTomorrow(), "yyyy-MM-dd"),
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
    .required("Start date is required")
    .typeError("Start date must be a valid date"),
  dueDate: Yup.date()
    .required("Due date is required")
    .min(Yup.ref("startDate"), "Due date cannot be before start date")
    .typeError("Due date must be a valid date"),
  priority: Yup.number()
    .required("Priority is required")
    .min(0, "Invalid priority")
    .max(4, "Priority cannot be greater than 4"),
  assignedTo: Yup.string().required("Assigned To is required"),
  assignedBy: Yup.string().required("Assigned By is required"),
  note: Yup.string().max(500, "Note must be at most 500 characters"),
});
