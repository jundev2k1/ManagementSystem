// Copyright (c) 2025 - Jun Dev. All rights reserved

import api from '../../api/axiosInstance';
import { setCredentials, logout } from './authSlice';
import { type AppDispatch } from '../../store/store';

const mapToAuthInfo = (data: any) => {
  const { userId, email, firstName, lastName, roles } = data;
  const auth = {
    userId,
    email,
    firstName,
    lastName,
    roles
  };
  return auth;
}

export const login = (userName: string, password: string) => async (dispatch: AppDispatch) => {
  const res = await api.post('/auth/login', { userName, password });
  const { token } = res.data;
  const auth = mapToAuthInfo(res.data);
  dispatch(setCredentials({ token: token, auth }));
};

export const register = (name: string, userName: string, password: string) =>
  async (dispatch: AppDispatch) => {
    const res = await api.post('/auth/register', { name, userName, password });

    const { token } = res.data;
    const auth = mapToAuthInfo(res.data)
    dispatch(setCredentials({ token, auth }));
  };

export const logoutUser = () => async (dispatch: AppDispatch) => {
  await api.post('/auth/logout');
  dispatch(logout());
};