// Copyright (c) 2025 - Jun Dev. All rights reserved

import * as Yup from "yup";

export const validationSchema = Yup.object({
  username: Yup.string().min(6, "Tối thiểu 6 ký tự").required("Bắt buộc"),
  password: Yup.string().min(8, "Tối thiểu 8 ký tự").required("Bắt buộc"),
});

export interface SignInFormInput {
  username: string;
  password: string;
}

export const initialValue : SignInFormInput = {
  username: '',
  password: '',
}