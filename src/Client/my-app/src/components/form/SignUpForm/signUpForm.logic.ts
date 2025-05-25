// Copyright (c) 2025 - Jun Dev. All rights reserved

import * as Yup from "yup";

export const validationSchema = Yup.object({
  username: Yup.string().min(6, "Tối thiểu 6 ký tự").required("Bắt buộc"),
  email: Yup.string().email("Định dạng Email chưa đúng").required("Bắt buộc"),
  password: Yup.string().min(8, "Tối thiểu 8 ký tự").required("Bắt buộc"),
  firstName: Yup.string().required("Bắt buộc"),
  lastName: Yup.string().required("Bắt buộc"),
  phoneNumber: Yup.string().matches(/^\+?[0-9]\d{1,14}$/, "Định dạng số điện thoại chưa đúng.").required("Bắt buộc"),
  address: Yup.string(),
});

export interface SignUpFormInput {
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  address: string;
}

export const initialValue: SignUpFormInput = {
  username: '',
  email: '',
  password: '',
  firstName: '',
  lastName: '',
  phoneNumber: '',
  address: '',
}
