// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useEffect, useState } from "react";
import type { FilterItem, SearchResult, SortItem } from "../../../common/types";
import { taskApi, type TaskModel } from "../../../api/services/task";
import { initSearchResult } from "../../../common/data";

export const TaskPage = Object.freeze({
  BLANK: 'blank',
  DETAIL: "detail",
  CREATE: "create",
  EDIT: "edit",
  DELETE: "delete",
  FILTER: "filter",
  SORT: "sort",
});

export const getModalName = (targetPage: string) => {
  switch (targetPage) {
    case TaskPage.DETAIL: return "Task information";
    case TaskPage.CREATE: return "Create new task";
    case TaskPage.EDIT: return "Edit task";
    case TaskPage.DELETE: return "Are you sure?";
    case TaskPage.FILTER: return "Advanced filters";
    case TaskPage.SORT: return "Sorting";
    default: return "";
  }
}

export const useTaskList = () => {
  const [keyword, setKeyword] = useState<string>('');
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
        setIsLoading(false);
      });
  }, [filters, sorts, page, pageSize]);

  return {
    isLoading,
    data,
    keyword,
    onKeywordChange: setKeyword,
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
