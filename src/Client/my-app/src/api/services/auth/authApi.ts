// Copyright (c) 2025 - Jun Dev. All rights reserved

import { api } from '../../client/fetchClient';
import type { AuthRequest, AuthResponse, RegisterRequest } from './types';

export const authApi = {
  login: ({ userName, password }: AuthRequest) => api.post<AuthResponse>('/auth/login', { userName, password }),
  register: (request: RegisterRequest) => api.post<AuthResponse>(`/auth/register`, request),
};
