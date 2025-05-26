// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Formik, Form } from "formik";
import { Input, Button } from "../../common";
import {
  initialValue,
  validationSchema,
  type SignInFormInput,
} from "./signInForm.logic";

type SignInFormParam = {
  onSubmit: (formValues: SignInFormInput, actions: any) => void;
};
const SignInForm = ({ onSubmit }: SignInFormParam) => (
  <Formik
    initialValues={initialValue}
    validationSchema={validationSchema}
    onSubmit={onSubmit}
  >
    {({ isSubmitting }) => (
      <Form className="max-w-xl mx-auto space-y-4">
        <div className="w-[300px]">
          <Input label="User name" name="userName" />
          <Input label="Password" name="password" type="password" />
          <div className="flex justify-center mt-[32px]">
            <Button type="submit" isLoading={isSubmitting}>
              Sign In
            </Button>
          </div>
        </div>
      </Form>
    )}
  </Formik>
);
export default SignInForm;
