// Copyright (c) 2025 - Jun Dev. All rights reserved

import { api } from "../../client/fetchClient";
import { getToken } from "../../client/tokenUtils";
import type { AuthRequest, AuthResponse, RegisterRequest } from "./types";

export const authApi = {
  login: ({ userName, password }: AuthRequest) =>
    api.post<AuthResponse>(
      "/auth/login",
      { userName, password },
      { auth: false, isRedirectError: false }
    ),
  logout: () => {
    const { refreshToken } = getToken();
    
    return refreshToken
      ? api.post<any>("/auth/logout", { refreshToken })
      : null;
  },
  register: (request: RegisterRequest) =>
    api.post<AuthResponse>("/auth/register", request, {
      auth: false,
      isRedirectError: false,
    }),
  refresh: () => {
    const { refreshToken } = getToken();

    return refreshToken
      ? api.post<AuthResponse>("/auth/refresh-token", { refreshToken })
      : null;
  },
};
