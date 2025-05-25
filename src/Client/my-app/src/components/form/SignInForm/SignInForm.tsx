// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Formik, Form } from "formik";
import { Input, Button } from "../../common";
import { initialValue, validationSchema, type SignInFormInput } from "./signInForm.logic";

type SignInFormParam = { onSubmit: (formValues: SignInFormInput, actions: any) => void };
const SignInForm = ({ onSubmit }: SignInFormParam) => (
  <Formik
    initialValues={initialValue}
    validationSchema={validationSchema}
    onSubmit={onSubmit}
  >
    {({ isSubmitting }) => (
      <Form className="max-w-sm mx-auto space-y-4">
        <Input label="User name" name="username" />
        <Input label="Password" name="password" type="password" />
        <Button type="submit" isLoading={isSubmitting}>
          Đăng nhập
        </Button>
      </Form>
    )}
  </Formik>
);
export default SignInForm;
