// Copyright (c) 2025 - Jun Dev. All rights reserved

import { api } from "../../client/fetchClient";
import type { SearchRequest, SearchResult } from "../../../common/types";
import type { TaskModel } from "./types";
import { toParameters } from "../../../common/helper";

export const taskApi = {
  getById: (id: string) => api.get<TaskModel>(`/task/${id}`),
  getByCriteria: (request: SearchRequest) => {
    debugger
    const params = toParameters(request);
    return api.get<SearchResult<TaskModel>>(`/task`, { params: params });
  },
};
