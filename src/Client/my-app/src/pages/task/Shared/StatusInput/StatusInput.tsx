// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Select, type SelectOption } from "../../../../components/common";

const statusOptions: SelectOption[] = [
  { label: "New", value: 0 },
  { label: "In-Progress", value: 1 },
  { label: "In-Review", value: 2 },
  { label: "Reviewed", value: 3 },
  { label: "Rejected", value: 4 },
  { label: "Completed", value: 5 },
  { label: "Pending", value: 6 },
  { label: "Cancelled", value: 7 },
];

export type StatusInputProps = {
  name: string;
  label: string;
};

const StatusInput = ({ name, label }: StatusInputProps) => {
  return <Select name={name} label={label} options={statusOptions} />;
};

export default StatusInput;
