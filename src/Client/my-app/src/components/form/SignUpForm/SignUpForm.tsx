// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Formik, Form } from "formik";
import { Input, Button } from "../../common";
import { initialValue, validationSchema, type SignUpFormInput } from "./signUpForm.logic";

type SignUpFormParam = { onSubmit: (formValues: SignUpFormInput, actions: any) => void };
const SignUpForm = ({ onSubmit }: SignUpFormParam) => (
  <Formik
    initialValues={initialValue}
    validationSchema={validationSchema}
    onSubmit={onSubmit}
  >
    {({ isSubmitting }) => (
      <Form className="max-w-sm mx-auto space-y-4">
        <Input label="Email" name="email" />
        <Input label="Họ" name="firstName" />
        <Input label="Tên" name="lastName" />
        <Input label="Số điện thoại" name="phoneNumber" />
        <Input label="Địa chỉ" name="address" />
        <Input label="Tài khoản" name="username" />
        <Input label="Mật khẩu" name="password" type="password" />
        <Button type="submit" isLoading={isSubmitting}>Đăng ký</Button>
      </Form>
    )}
  </Formik>
);
export default SignUpForm;
