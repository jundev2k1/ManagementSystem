// Copyright (c) 2025 - Jun Dev. All rights reserved

import { type TaskModel } from "../../../api/services";
import { formatDate } from "../../../common/utils/datetime";
import { Label } from "../../../components/common";
import { UserPicker } from "../../user";
import { StatusBadge as TaskStatusBadge } from "../Shared";

type BodyDetailTaskProps = {
  data: TaskModel;
};

const BodyDetailTask = ({ data }: BodyDetailTaskProps) => {
  const {
    title,
    description,
    progress,
    status,
    startDate,
    dueDate,
    actualStartDate,
    actualEndDate,
    assignedTo,
    assignedBy,
    note,
  } = data;
  return (
    <div className="mx-auto px-4 max-h-[75vh] overflow-y-auto">
      <div className="grid grid-cols-6 gap-4">
        <div className="col-span-6">
          <Label>Title</Label>
          <p>{title}</p>
        </div>
        <div className="col-span-6">
          <Label>Description</Label>
          <p>{description}</p>
        </div>
        <div className="col-span-3">
          <Label>Progress</Label>
          <p>{progress}%</p>
        </div>
        <div className="col-span-3">
          <Label>Status</Label>
          <p>
            <TaskStatusBadge status={status} />
          </p>
        </div>
        <div className="col-span-3">
          <Label>Start Date</Label>
          <p className={!startDate ? "text-gray-400" : undefined}>
            {startDate ? formatDate(startDate) : "Not set"}
          </p>
        </div>
        <div className="col-span-3">
          <Label>Due Date</Label>
          <p className={!dueDate ? "text-gray-400" : undefined}>
            {dueDate ? formatDate(dueDate) : "Not set"}
          </p>
        </div>
        <div className="col-span-3">
          <Label>Actual Start Date</Label>
          <p className={!actualStartDate ? "text-gray-400" : undefined}>
            {actualStartDate ? formatDate(actualStartDate) : "Not set"}
          </p>
        </div>
        <div className="col-span-3">
          <Label>Actual End Date</Label>
          <p className={!actualEndDate ? "text-gray-400" : undefined}>
            {actualEndDate ? formatDate(actualEndDate) : "Not set"}
          </p>
        </div>
        <div className="col-span-3">
          <p>
            <UserPicker label="Assigned By" defaultValue={assignedBy} isView />
          </p>
        </div>
        <div className="col-span-3">
          <p>
            <UserPicker label="Assigned To" defaultValue={assignedTo} isView />
          </p>
        </div>
        <div className="col-span-6">
          <Label>Note</Label>
          <p className={!note ? "text-gray-400" : undefined}>{note || "No notes"}</p>
        </div>
      </div>
    </div>
  );
};

export default BodyDetailTask;
