// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { Button, toast } from "../../components/common";
import "./AuthPage.css";
import { useAuth } from "../../features";
import { SignUpForm, SignInForm, type SignUpFormInput, type SignInFormInput } from "../../components/form";

const AuthPage = () => {
  const [isSignInPage, setIsSignInPage] = useState(true);
  const { onLogin, onRegister } = useAuth();

  const onChangePage = () => setIsSignInPage(!isSignInPage);

  const onSignInSubmit = async (formInput: SignInFormInput, actions: any) => {
    try {
      await onLogin(formInput);
    } catch {
      toast.error('Incorrect account or password');
    } finally {
      actions.setSubmitting(false);
    }
  }

  const onSignUpSubmit = async (formInput: SignUpFormInput, actions: any) => {
    try {
      await onRegister(formInput);
    } catch {
      toast.error('Username or email is exists');
    } finally {
      actions.setSubmitting(false);
    }
  }

  return (
    <div id="sign-in-wrapper" className="w-100 h-[100vh] flex">
      <div className="w-[75%]">
        <div id="main-content" className="flex flex-col h-full items-center justify-center">
          <h1 className="mb-[32px] text-5xl text-bold">{isSignInPage ? "Login to Your Account" : "Register new Account"}</h1>
          {isSignInPage ? <SignInForm onSubmit={onSignInSubmit} /> : <SignUpForm onSubmit={onSignUpSubmit} />}

        </div>
      </div>
      <div className="w-[25%] bg-green-300">
        <div id="sub-content" className="flex flex-col h-full items-center justify-center">
          <h2 className="text-2xl mb-2">{isSignInPage ? "New Here?" : "Already have an account"}</h2>
          <div className="text-center">
            <Button color="gray" onClick={onChangePage}>{isSignInPage ? "Sign Up" : "Sign In"}</Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default AuthPage;