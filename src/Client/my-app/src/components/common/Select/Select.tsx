// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useField } from "formik";
import { Label, Select as FlowbiteSelect } from "flowbite-react";

export type SelectOption = {
  value: string | number;
  label: string;
};

type SelectProps = {
  label: string;
  name: string;
  options: SelectOption[];
  placeholder?: string;
};

const Select = ({
  label,
  name,
  options,
  placeholder = "Choose...",
}: SelectProps) => {
  const [field, meta] = useField(name);
  const showError = meta.touched && meta.error;

  return (
    <div className="mb-1">
      <Label htmlFor={name}>{label}</Label>
      <FlowbiteSelect
        {...field}
        id={name}
        name={name}
        color={showError ? "failure" : undefined}
      >
        {placeholder && <option value="">{placeholder}</option>}
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.label}
          </option>
        ))}
      </FlowbiteSelect>
      {showError && (
        <p className="mt-1 text-sm text-red-600">{meta.error}</p>
      )}
    </div>
  );
};

export default Select;
