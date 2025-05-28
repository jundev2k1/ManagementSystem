// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Badge } from "flowbite-react";

const taskStatusDisplayes = {
  0: ["New", "info"],
  1: ["In-Progress", "yellow"],
  2: ["In-Review", "purple"],
  3: ["Reviewed", "indigo"],
  4: ["Rejected", "red"],
  5: ["Completed", "green"],
  6: ["Pending", "gray"],
  7: ["Cancelled", "dark"],
};

export type taskStatusValues = 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7;

export type StatusBadgeProps = {
  status: taskStatusValues;
};

const StatusBadge = ({ status }: StatusBadgeProps) => {
  const [content, color] = taskStatusDisplayes[status];
  if (!content) return <></>;

  return <Badge className="inline-flex" color={color} size="sm">{content}</Badge>;
};

export default StatusBadge;
