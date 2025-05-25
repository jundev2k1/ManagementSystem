// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../../store/store';
import { login, logoutUser } from './authThunks';

export const useAuth = () => {
  const dispatch: AppDispatch = useDispatch();
  const { auth, token, isAuthenticated } = useSelector((state: RootState) => state.auth);

  return {
    auth,
    token,
    isAuthenticated,
    login: (userName: string, password: string) => dispatch(login(userName, password)),
    logout: () => dispatch(logoutUser()),
  };
};