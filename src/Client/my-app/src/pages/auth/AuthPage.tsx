// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { SignInForm } from "../../components/form/SignInForm";
import { Button } from "../../components/common";
import './authPage.css';
import { useAuth } from "../../features";
import type { SignInFormInput } from "../../components/form/SignInForm/signInForm.logic";
import { SignUpForm } from "../../components/form/SignUpForm";
import type { SignUpFormInput } from "../../components/form/SignUpForm/signUpForm.logic";

const AuthPage = () => {
  const [isSignInPage, setIsSignInPage] = useState(true);
  const { onLogin, onRegister } = useAuth();

  const onChangePage = () => setIsSignInPage(!isSignInPage);

  const onSignInSubmit = async ({ username, password }: SignInFormInput, actions: any) => {
    try {
      await onLogin(username, password);
    } catch {
      console.log('error');
    } finally {
      actions.setSubmitting(false);
    }
  }

  const onSignUpSubmit = async (formInput: SignUpFormInput, actions: any) => {
    try {
      await onRegister(formInput);
    } catch {
      console.log('error');
    } finally {
      actions.setSubmitting(false);
    }
  }

  return (
    <div id="sign-in-wrapper" className="w-100 h-[100vh] flex">
      <div className="w-[75%]">
        <div id="main-content" className="flex h-full items-center justify-center">
          {isSignInPage ? <SignInForm onSubmit={onSignInSubmit} /> : <SignUpForm onSubmit={onSignUpSubmit} />}
        </div>
      </div>
      <div className="w-[25%] bg-green-300">
        <div id="sub-content" className="flex h-full items-center justify-center">
          <Button onClick={onChangePage}>{isSignInPage ? "Sign Up" : "Sign In"}</Button>
        </div>
      </div>
    </div>
  )
}

export default AuthPage;