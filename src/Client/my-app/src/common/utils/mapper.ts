// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { ParameterInfo } from "../../api/client/types";
import type { SearchRequest } from "../types";

export const toParameters = ({ filters, sorts, page,  pageSize }:SearchRequest): ParameterInfo[] => {
  const filterParams = filters
    .filter((item) => item.field && item.operator && item.value)
    .map(({ field, operator, value }) => ({
      key: "filter",
      value: `${field}~${operator}~${value}`,
    }));

  const sortParams = sorts.map(({ field, direction }) => ({
    key: "sort",
    value: `${field}:${direction}`,
  }));
  return [
    ...filterParams,
    ...sortParams,
    { key: "page", value: page },
    { key: "pageSize", value: pageSize },
  ];
};
