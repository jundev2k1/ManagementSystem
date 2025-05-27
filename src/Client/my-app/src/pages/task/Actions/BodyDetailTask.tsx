// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Label } from "../../../components/common";
import { type TaskModel } from "../../../api/services/task";
import { formatDate } from "../../../common/utils/datetime";

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
    assignedTo,
    assignedBy,
    note,
  } = data;
  return (
    <div className="mx-auto px-4 max-h-[75vh] overflow-y-auto">
      <div className="grid grid-cols-6 gap-2">
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
          <p>{progress}</p>
        </div>
        <div className="col-span-3">
          <Label>Status</Label>
          <p>{status}</p>
        </div>
        <div className="col-span-3">
          <Label>Start Date</Label>
          <p>{formatDate(startDate)}</p>
        </div>
        <div className="col-span-3">
          <Label>Due Date</Label>
          <p>{formatDate(dueDate)}</p>
        </div>
        <div className="col-span-3">
          <Label>Assigned To</Label>
          <p>{assignedTo}</p>
        </div>
        <div className="col-span-3">
          <Label>Assigned By</Label>
          <p>{assignedBy}</p>
        </div>
        <div className="col-span-6">
          <Label>Note</Label>
          <p>{note}</p>
        </div>
      </div>
    </div>
  );
};

export default BodyDetailTask;
