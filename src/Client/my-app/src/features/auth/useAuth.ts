// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../../store/store';
import { login, logoutUser } from './authThunks';

export const useAuth = () => {
  const dispatch: AppDispatch = useDispatch();
  const { auth, token, isAuthenticated } = useSelector((state: RootState) => state.auth);

  const onLogin = async (userName: string, password: string) => {
    try {
      await dispatch(login(userName, password));
      return true;
    }
    catch {
      return false;
    }
  }

  return {
    auth,
    token,
    isAuthenticated,
    login: onLogin,
    logout: () => dispatch(logoutUser()),
  };
};