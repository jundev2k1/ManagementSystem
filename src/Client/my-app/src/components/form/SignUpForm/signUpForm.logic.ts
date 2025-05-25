// Copyright (c) 2025 - Jun Dev. All rights reserved

import * as Yup from "yup";

export const validationSchema = Yup.object({
  username: Yup.string().min(6, "Minimum 6 characters").required("Required"),
  email: Yup.string().email("Invalid email format").required("Required"),
  password: Yup.string().min(8, "Minimum 8 characters").required("Required"),
  firstName: Yup.string().required("Required"),
  lastName: Yup.string().required("Required"),
  phoneNumber: Yup.string().matches(/^\+?[0-9]\d{1,14}$/, "Invalid phone number format").required("Required"),
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
