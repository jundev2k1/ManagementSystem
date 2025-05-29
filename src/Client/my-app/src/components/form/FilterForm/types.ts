// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { FilterOperator } from "../../../common/types";

export const filterOperator: Record<string, FilterOperator[]> = {
  TEXT: ["eq", "ne", "c", "sw", "ew"],
  NUMBER: ["eq", "ne", "gt", "gte", "lt", "lte", "c", "sw", "ew"],
  DATETIME: ["gt", "gte", "lt", "lte"],
  BOOLEAN: ["eq", "ne"],
  ENUM: ["eq", "ne"],
  USER: ["eq", "ne"],
};

export type FilterInputType =
  | "text"
  | "number"
  | "datetime"
  | "boolean"
  | "enum"
  | "user";

export interface FilterOptions {
  field: string;
  display: string;
  type: FilterInputType;
  dataSource?: Record<string, string>;
  defaultOperator?: FilterOperator;
  defaultValue?: string;
  isDefault?: boolean;
}
