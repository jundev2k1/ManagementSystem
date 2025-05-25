// Copyright (c) 2025 - Jun Dev. All rights reserved

import axios from 'axios';
import { store } from '../store/store';

const api = axios.create({
  baseURL: 'http://localhost:5068',
  withCredentials: true,
});

// Interceptor: tự gắn token
api.interceptors.request.use((config) => {
  const token = store.getState().auth.token;
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

api.interceptors.response.use(
  response => response,
  error => {
    if (axios.isAxiosError(error)) {
      const message = error.response?.data?.message || "Có lỗi xảy ra";
      console.error("❌ API error:", message);
    }
    return Promise.reject(error);
  }
);

export default api;