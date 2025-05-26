// Copyright (c) 2025 - Jun Dev. All rights reserved

import { getToken } from './tokenUtils';
import { handleError } from './errorHandler';

const baseURL: string = import.meta.env.VITE_API_URL || 'http://localhost:5068';

export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';

interface FetchOptions extends RequestInit {
  auth?: boolean;
  params?: ParameterInfo[];
}

export interface ParameterInfo {
  key: string,
  value: any,
}

interface ApiResult<T> {
  data: T | null;
  message: string;
  isSuccess: boolean;
}

const buildUrl = (url: string, params?: ParameterInfo[]) => {
  const query = params ? '?' + params.map(({ key, value }) => `${key}=${value}`).join('&') : '';
  return `${url}${query}`;
};

export const callApi = async <T>(
  method: HttpMethod,
  url: string,
  body?: any,
  options: FetchOptions = {}
): Promise<ApiResult<T>> => {
  const { auth = true, headers, params, ...rest } = options;
  const token = auth ? getToken() : null;

  const result: ApiResult<T> = {
    data: null,
    message: '',
    isSuccess: false,
  };

  try {
    const endpoint = buildUrl(baseURL + url, params);
    const option = {
      method,
      headers: {
        'Content-Type': 'application/json',
        ...(auth && token ? { Authorization: `Bearer ${token}` } : {}),
        ...(headers || {}),
      },
      body: body ? JSON.stringify(body) : undefined,
      ...rest
    };
    const res = await fetch(endpoint, option);

    if (!res.ok) {
      const error = await handleError(res);
      throw error;
    }

    const json = res.status !== 204 ? await res.json() : {};
    return {
      ...result,
      data: json.data || null,
      message: json.message,
      isSuccess: json.success,
    };
  } catch (err: any) {
    return {
      ...result,
      message: err,
      isSuccess: false,
    };
  }
};

export const api = {
  get: <T>(url: string, options?: FetchOptions) => callApi<T>('GET', url, null, options),
  post: <T>(url: string, body: any, options?: FetchOptions) => callApi<T>('POST', url, body, options),
  put: <T>(url: string, body: any, options?: FetchOptions) => callApi<T>('PUT', url, body, options),
  delete: <T>(url: string, options?: FetchOptions) => callApi<T>('DELETE', url, null, options),
};