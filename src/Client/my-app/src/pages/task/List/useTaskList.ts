// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useEffect, useState } from "react";
import type { FilterItem, SearchResult, SortItem } from "../../../common/types";
import type { TaskModel } from "../../../api/services/task/types";
import { taskApi } from "../../../api/services/task/taskApi";

export const useTaskList = () => {
  const [data, setData] = useState<SearchResult<TaskModel>>(dummy);
  const [filters, setFilters] = useState<FilterItem[]>([]);
  const [sorts, setSorts] = useState<SortItem[]>([]);
  const [page, setPage] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(20);

  useEffect(() => {
    debugger
    taskApi
      .getByCriteria({ filters, sorts, keyword: "", page, pageSize })
      .then((res) => {
        if (res.success && res.data != null) setData(res.data);
      });
  }, [filters, sorts, page, pageSize]);

  return {
    data,
    filters,
    setFilters,
    sorts,
    setSorts,
    page,
    setPage,
    pageSize,
    setPageSize,
  };
};

var dummy: SearchResult<TaskModel> = {
  items: [
    {
      title: "Task Demo 01",
      description: "Demo 01",
      progress: 33,
      status: 1,
      assignedBy: "Dummy Bro 1",
      assignedTo: "Dummy Bro 2",
      CreatedAt: new Date('2025-12-12'),
      CreatedBy: "Dummy",
      startDate: new Date('2025-12-12'),
      dueDate: new Date('2025-12-12'),
      LastModifiedAt: new Date('2025-12-12'),
      LastModifiedBy: "Dummy",
      note: "",
      priority: 1,
      taskId: ""
    },
    {
      title: "Task Demo 02",
      description: "Demo 02",
      progress: 60,
      status: 3,
      assignedBy: "Dummy 1",
      assignedTo: "Dummy 2",
      CreatedAt: new Date('2025-12-12'),
      CreatedBy: "Dummy",
      startDate: new Date('2025-12-12'),
      dueDate: new Date('2025-12-12'),
      LastModifiedAt: new Date('2025-12-12'),
      LastModifiedBy: "Dummy",
      note: "",
      priority: 1,
      taskId: ""
    }
  ],
  totalItems: 2,
  totalPage: 1,
  pageSize: 1,
  pageNumber: 1,
};