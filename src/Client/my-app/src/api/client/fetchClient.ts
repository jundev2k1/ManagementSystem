// Copyright (c) 2025 - Jun Dev. All rights reserved

import { getToken } from './tokenUtils';
import { handleError } from './errorHandler';
import type { ApiResult, FetchOptions, HttpMethod, ParameterInfo } from './types';

const baseURL: string = import.meta.env.VITE_API_URL || 'http://localhost:5068';

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
  const { auth = true, headers, params, isRedirectError = true, ...rest } = options;
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
        ...(auth && token?.accessToken ? { Authorization: `Bearer ${token.accessToken}` } : {}),
        ...(headers || {}),
      },
      body: body ? JSON.stringify(body) : undefined,
      ...rest
    };
    const res = await fetch(endpoint, option);

    if (!res.ok) {
      const error = await handleError(res, isRedirectError);
      if (error) throw error;

      return callApi<T>(method, url, body, option);
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