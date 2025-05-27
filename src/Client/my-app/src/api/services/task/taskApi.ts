// Copyright (c) 2025 - Jun Dev. All rights reserved

import { api } from "../../client/fetchClient";
import type { SearchRequest, SearchResult } from "../../../common/types";
import type { TaskModel } from "./types";
import { toParameters } from "../../../common/utils/mapper";

export const taskApi = {
  getById: (id: string) => api.get<TaskModel>(`/task/${id}`),
  getByCriteria: (request: SearchRequest) => {
    const params = toParameters(request);
    return api.get<SearchResult<TaskModel>>(`/task`, { params: params });
  },
  create: (request: TaskModel) => api.post<string>('/task', request),
  update: (request: TaskModel) => api.put<string>(`/task/${request.taskId}`, request),
  delete: (id: string) => api.delete<void>(`/task/${id}`),
};
