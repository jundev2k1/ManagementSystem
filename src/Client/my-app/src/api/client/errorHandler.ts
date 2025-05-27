// Copyright (c) 2025 - Jun Dev. All rights reserved

import { store } from "../../store/store";
import type { AuthResponse } from "../services/auth";
import { clearToken, getToken, setToken } from "./tokenUtils";
import type { ApiResult } from "./types";

const baseURL: string = import.meta.env.VITE_API_URL || "http://localhost:5068";

const refreshAccessToken = async (isRedirectError = true) => {
  try {
    const { accessToken, refreshToken } = getToken();
    if (!refreshToken) return false;

    const option = {
      method: "POST",
      headers: {
        Authorization: `Bearer ${accessToken}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ refreshToken }),
    };
    const res = await fetch(`${baseURL}/auth/refresh-token`, option);

    if (res.status === 401) {
      clearToken();
      // Optional: redirect to login
      window.location.href = "/login";
      return false;
    }

    const { data, isSuccess, message }: ApiResult<AuthResponse> = await res.json();
    if (res.status !== 200 || isSuccess === false) {
      if (!isRedirectError) return false;

      window.localStorage.setItem("errorMessage", message);
      window.location.href = "/error";
    }

    if (!data) return false;

    const state = store.getState();
    state.auth.auth = {
      userId: data.userId,
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
      roles: data.roles,
    };
    setToken(data.accessToken, data.refreshToken);
    return true;
  } catch {
    return false;
  }
};

export const handleError = async (res: Response, isRedirectError = true): Promise<Error | null> => {
  if (res.status === 401) {
    const isSuccess = await refreshAccessToken(isRedirectError);
    if (isSuccess) return null;

    return new Error("Unauthorized");
  }

  const message = await res.text();
  return new Error(message || `Request failed with status ${res.status}`);
};
