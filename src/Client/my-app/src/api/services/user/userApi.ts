// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { SearchRequest, SearchResult } from "../../../common/types";
import { toParameters } from "../../../common/utils/mapper";
import { api } from "../../client/fetchClient";
import type { UserModel } from "./types";

export const userApi = {
  getById: (id: string) => api.get<UserModel>(`/users/${id}`),
  getByCriteria: (request: SearchRequest) => {
    const params = toParameters(request);
    return api.get<SearchResult<UserModel>>(`/users`, { params: params });
  },
  create: (request: UserModel) => api.post<string>("/users", request),
  update: (request: UserModel) => api.put<string>(`/users/${request.userId}`, request),
  delete: (id: string) => api.delete<void>(`/users/${id}`),
};
