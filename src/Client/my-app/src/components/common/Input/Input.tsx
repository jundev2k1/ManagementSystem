// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Label, TextInput } from "flowbite-react";
import { useField } from "formik";

interface InputProps {
  label: string;
  name: string;
  type?: string;
  placeholder?: string;
  autoComplete?: string;
}

const CommonInput = ({
  label,
  name,
  type = "text",
  placeholder = "",
  autoComplete = "off",
}: InputProps) => {
  const [field, meta] = useField(name);
  const showError = meta.touched && meta.error;

  return (
    <div className="mb-4">
      <Label htmlFor={name}>{label}</Label>
      <TextInput
        {...field}
        id={name}
        name={name}
        type={type}
        placeholder={placeholder}
        autoComplete={autoComplete}
        color={showError ? "failure" : undefined}
      />
      {showError && (
        <p className="mt-1 text-sm text-red-600">{meta.error}</p>
      )}
    </div>
  );
};
export default CommonInput;