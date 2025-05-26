// Copyright (c) 2025 - Jun Dev. All rights reserved

import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

interface AuthInfo {
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}

interface AuthState {
  token: string | null;
  auth: AuthInfo | null;
  isAuthenticated: boolean;
}

const initialState : AuthState = {
  token: null,
  auth: null,
  isAuthenticated: false,
}

const authSlice = createSlice({
  name: 'auth',
  initialState: initialState,
  reducers: {
    setCredentials: (
      state,
      action: PayloadAction<{ auth: AuthInfo }>
    ) => {
      state.auth = action.payload.auth;
      state.isAuthenticated = true;
    },
    logout: (state) => {
      state.auth = null;
      state.isAuthenticated = false;
    },
  },
});

export const { setCredentials, logout } = authSlice.actions;
export default authSlice.reducer;
