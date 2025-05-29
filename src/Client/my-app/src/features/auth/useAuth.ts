// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../../store/store';
import type { SignUpFormInput } from '../../components/form/SignUpForm/signUpForm.logic';
import { authApi, type RegisterRequest } from '../../api/services';
import { logout, setCredentials } from './authSlice';
import { clearToken, setToken } from '../../api/client/tokenUtils';
import type { LoginInputRequest } from './types';

export const useAuth = () => {
  const dispatch: AppDispatch = useDispatch();
  const { auth, token, isAuthenticated } = useSelector((state: RootState) => state.auth);

  const onLogin = async ({userName, password} : LoginInputRequest) => {
    // Handle authentication submission
    const res = await authApi.login({ userName, password });
    if (!res.isSuccess || (res.data == null)) throw Error();

    // Handle registration submission
    const { userId, email, firstName, lastName, roles, accessToken, refreshToken } = res.data;
    setToken(accessToken, refreshToken);
    dispatch(setCredentials({ auth: { userId: userId, email: email, firstName: firstName, lastName: lastName, roles } }));
  }

  const onRegister = async (formInput: SignUpFormInput) => {
    // Handle registration submission
    const request: RegisterRequest = {
      userName: formInput.username,
      email: formInput.email,
      password: formInput.password,
      firstName: formInput.firstName,
      lastName: formInput.lastName,
      phoneNumber: formInput.phoneNumber,
      address: formInput.address,
    };
    const res = await authApi.register(request);
    if (!res.isSuccess || (res.data == null)) throw Error();

    // Handle if complete
    const { userId, email, firstName, lastName, roles, accessToken, refreshToken } = res.data;
    setToken(accessToken, refreshToken);
    dispatch(setCredentials({ auth: { userId: userId, email: email, firstName: firstName, lastName: lastName, roles } }));
  }

  const onLogout = async () => {
    await authApi.logout();

    clearToken();
    dispatch(logout());
  }

  return {
    auth,
    token,
    isAuthenticated,
    onLogin: onLogin,
    onRegister: onRegister,
    onLogout: onLogout,
  };
};