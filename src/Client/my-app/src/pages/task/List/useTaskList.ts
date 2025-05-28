// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { taskApi, type TaskModel } from "../../../api/services/task";
import { initSearchResult } from "../../../common/data";
import type { FilterItem, SearchResult, SortItem } from "../../../common/types";
import type { FilterOptions } from "../../../components/form/FilterForm/types";
import type { SortOptions } from "../../../components/form/SortForm/types";
import type { RootState } from "../../../store/store";

export const TaskPage = Object.freeze({
  BLANK: "blank",
  DETAIL: "detail",
  CREATE: "create",
  EDIT: "edit",
  DELETE: "delete",
  FILTER: "filter",
  SORT: "sort",
});

export const getModalName = (targetPage: string) => {
  switch (targetPage) {
    case TaskPage.DETAIL:
      return "Task Information";
    case TaskPage.CREATE:
      return "Create New Task";
    case TaskPage.EDIT:
      return "Edit Task";
    case TaskPage.DELETE:
      return "";
    case TaskPage.FILTER:
      return "Advanced Filters";
    case TaskPage.SORT:
      return "Sorting";
    default:
      return "";
  }
};

export const filterSettings: FilterOptions[] = [
  {
    field: "title",
    display: "Title Task",
    type: "text",
  },
  {
    field: "startDate",
    display: "Start Date",
    type: "datetime",
  },
  {
    field: "dueDate",
    display: "Due Date",
    type: "datetime",
  },
  {
    field: "progress",
    display: "Task Progress",
    type: "number",
  },
  {
    field: "createdBy",
    display: "Created By",
    type: "text",
    isDefault: true,
  },
];

export const sortSettings: SortOptions[] = [
  {
    field: "title",
    display: "Title Task",
    defaultDirection: "asc",
  },
  {
    field: "startDate",
    display: "Start Date",
  },
  {
    field: "endDate",
    display: "End Date",
  },
  {
    field: "process",
    display: "Task Process",
  }
];

export const useTaskList = () => {
  const auth = useSelector((state: RootState) => state.auth.auth);
  const defaultFilters: FilterItem[] = [
    {
      field: "createdBy",
      operator: "eq",
      value: auth?.userId || "",
    },
  ];

  const [keyword, setKeyword] = useState<string>("");
  const [filters, setFilters] = useState<FilterItem[]>(defaultFilters);
  const [sorts, setSorts] = useState<SortItem[]>([]);
  const [page, setPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);
  const [isLoading, setIsLoading] = useState(true);
  const [data, setData] = useState<SearchResult<TaskModel>>(initSearchResult);
  const [refreshData, setRefreshData] = useState(0);

  useEffect(() => {
    taskApi
      .getByCriteria({ filters, sorts, keyword: "", page, pageSize })
      .then((res) => {
        setData(res.data!);
        setIsLoading(false);
      });
  }, [filters, sorts, page, pageSize, refreshData]);

  return {
    isLoading,
    data,
    onRefreshList: () => setRefreshData(refreshData + 1),
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
