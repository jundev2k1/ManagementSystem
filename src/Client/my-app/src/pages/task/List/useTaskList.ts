// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useEffect, useState } from "react";
import type { FilterItem, SearchResult, SortItem } from "../../../common/types";
import { taskApi, type TaskModel } from "../../../api/services/task";
import { initSearchResult } from "../../../common/data";

export const useTaskList = () => {
  const [filters, setFilters] = useState<FilterItem[]>([]);
  const [sorts, setSorts] = useState<SortItem[]>([]);
  const [page, setPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<SearchResult<TaskModel>>(initSearchResult);

  useEffect(() => {
    taskApi
      .getByCriteria({ filters, sorts, keyword: "", page, pageSize })
      .then((res) => {
        setData(res.data!);
        setIsLoading(true);
      });
  }, [filters, sorts, page, pageSize]);

  return {
    isLoading,
    data,
    filters,
    onFilterChange: setFilters,
    sorts,
    onSortChange: setSorts,
    page,
    onPageChange: setPage,
    pageSize,
    onPageSizeChange: setPageSize,
  };
};
