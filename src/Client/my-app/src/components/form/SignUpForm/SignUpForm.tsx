// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Formik, Form } from "formik";
import { Input, Button } from "../../common";
import {
  initialValue,
  validationSchema,
  type SignUpFormInput,
} from "./signUpForm.logic";

type SignUpFormParam = {
  onSubmit: (formValues: SignUpFormInput, actions: any) => void;
};
const SignUpForm = ({ onSubmit }: SignUpFormParam) => (
  <Formik
    initialValues={initialValue}
    validationSchema={validationSchema}
    onSubmit={onSubmit}
  >
    {({ isSubmitting }) => (
      <Form className="max-w-sm mx-auto">
        <div className="grid grid-cols-2 gap-2">
          <div className="col-span-2">
            <Input label="Tài khoản" name="username" />
          </div>
          <div className="col-span-2">
            <Input label="Mật khẩu" name="password" type="password" />
          </div>
          <div className="col-span-2">
            <Input label="Email" name="email" />
          </div>
          <div>
            <Input label="Họ" name="firstName" />
          </div>
          <div>
            <Input label="Tên" name="lastName" />
          </div>
          <div className="col-span-2">
            <Input label="Số điện thoại" name="phoneNumber" />
          </div>
          <div className="col-span-2">
            <Input label="Địa chỉ" name="address" />
          </div>
        </div>
        <div className="flex justify-center mt-4">
          <Button type="submit" isLoading={isSubmitting}>
            Sign up
          </Button>
        </div>
      </Form>
    )}
  </Formik>
);
export default SignUpForm;
