// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { SortDirection } from "../../../common/types";

export interface SortOptions {
  field: string;
  display: string;
  defaultDirection?: SortDirection;
  isDefault?: boolean;
}

export const sortDirections: readonly SortDirection[] = Object.freeze(["asc", "desc"]);
