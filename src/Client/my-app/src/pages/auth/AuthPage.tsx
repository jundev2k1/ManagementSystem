// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { SignInForm } from "../../components/form/SignInForm";
import { Button } from "../../components/common";
import './authPage.css';

const AuthPage = () => {
  const [isSignInPage, setIsSignInPage] = useState(true);

  const onChangePage = () => setIsSignInPage(!isSignInPage);

  return (
    <div id="sign-in-wrapper" className="w-100 h-[100vh] flex">
      <div className="w-[75%]">
        <div id="main-content" className="flex h-full items-center justify-center">
          <SignInForm onSubmit={(values) => console.log(values)} />
        </div>
      </div>
      <div className="w-[25%] bg-green-300">
        <div id="sub-content" className="flex h-full items-center justify-center">
          <Button onClick={onChangePage}>{isSignInPage ? "Sign Up" : "Sign In" }</Button>
        </div>
      </div>
    </div>
  )
}

export default AuthPage;