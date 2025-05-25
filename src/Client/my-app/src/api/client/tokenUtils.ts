// Copyright (c) 2025 - Jun Dev. All rights reserved

export const getToken = () => localStorage.getItem('access_token');
export const setToken = (token: string) => localStorage.setItem('access_token', token);
export const clearToken = () => localStorage.removeItem('access_token');