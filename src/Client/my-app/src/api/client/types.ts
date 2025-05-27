// Copyright (c) 2025 - Jun Dev. All rights reserved

export interface FetchOptions extends RequestInit {
  auth?: boolean;
  params?: ParameterInfo[];
  isRedirectError?: boolean;
}

export interface ParameterInfo {
  key: string,
  value: any,
}

export interface ApiResult<T> {
  data: T | null;
  message: string;
  isSuccess: boolean;
}

export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE';
