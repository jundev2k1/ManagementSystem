// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button } from "flowbite-react";
import { useAuth } from "../../../features";

const LoginPage = () => {

  const { auth, login } = useAuth();

  const onSubmit = async () => {
    const isSuccess = await login("administrator", "123456789");
    if (isSuccess)
    console.log(auth);
  }

  return (
  <div>
    <Button onClick={onSubmit}>Click Me!</Button>
  </div>
  )
}

export default LoginPage;