// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useSelector, useDispatch } from 'react-redux';
import type { RootState, AppDispatch } from '../../store/store';
import type { SignUpFormInput } from '../../components/form/SignUpForm/signUpForm.logic';
import { authApi, type RegisterRequest } from '../../api/services/auth';
import { logout, setCredentials } from './authSlice';
import { clearToken } from '../../api/client/tokenUtils';

export const useAuth = () => {
  const dispatch: AppDispatch = useDispatch();
  const { auth, token, isAuthenticated } = useSelector((state: RootState) => state.auth);

  const onLogin = async (userName: string, password: string) => {
    // const res = await authApi.login({ userName, password });
    // if (!res.success || (res.data == null)) throw Error();

    //const { userId, email, firstName, lastName, roles, token } = res.data;

    const token = "Dummy";
    const userId = "user1";
    const email = "a@g.c";
    const firstName = userName;
    const lastName = password;
    const roles = ["role1", "role2"];

    dispatch(setCredentials({ token, auth: { userId: userId, email: email, firstName: firstName, lastName: lastName, roles } }));
  }

  const onRegister = async (formInput: SignUpFormInput) => {
    debugger
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
    if (!res.success || (res.data == null)) throw Error();

    const { userId, email, firstName, lastName, roles, token } = res.data;
    dispatch(setCredentials({ token, auth: { userId: userId, email: email, firstName: firstName, lastName: lastName, roles } }));
  }

  const onLogout = () => {
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