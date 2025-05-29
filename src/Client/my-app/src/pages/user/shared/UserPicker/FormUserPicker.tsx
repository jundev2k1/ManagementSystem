// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Avatar, Button, Label, Spinner, TextInput } from "flowbite-react";
import { useField, useFormikContext } from "formik";
import { useEffect, useState } from "react";
import { FaCircleXmark } from "react-icons/fa6";
import { userApi, type UserModel } from "../../../../api/services";

type FormUserPickerProps = {
  name: string;
  label?: string;
};

const FormUserPicker = ({ name, label = "" }: FormUserPickerProps) => {
  const [field, meta] = useField<string>(name);
  const { setFieldValue } = useFormikContext();
  const [selectedUser, setSelectedUser] = useState<UserModel | null>(null);
  const [query, setQuery] = useState("");
  const [suggestions, setSuggestions] = useState<UserModel[]>([]);
  const [loading, setLoading] = useState(false);
  const showError = meta.touched && meta.error;

  // Init fetch user
  useEffect(() => {
    if (!selectedUser && field.value) {
      setLoading(true);
      userApi.getById(field.value)
        .then((res) => setSelectedUser(res.data))
        .finally(() => setLoading(false));
    }
  }, [field.value]);

  // Fetch Suggestion users
  useEffect(() => {
    if (!query) {
      setSuggestions([]);
      return;
    }
    const timeout = setTimeout(() => {
      userApi.getByCriteria({ keyword: query, filters: [], sorts: [], page: 1, pageSize: 5 }).then((res) =>
        setSuggestions(res.data?.items || [])
      );
    }, 300);
    return () => clearTimeout(timeout);
  }, [query]);

  const handleSelect = (user: UserModel) => {
    setSelectedUser(user);
    setFieldValue(name, user.userId);
    setSuggestions([]);
    setQuery("");
  };

  const handleClear = () => {
    setSelectedUser(null);
    setFieldValue(name, "");
  };

  const handleCloseSuggestion = () => {
    const timeout = setTimeout(() => {
      setSuggestions([]);
      clearTimeout(timeout);
    }, 300);
  }

  return (
    <div className="relative">
      {label && <Label>{label}</Label>}
      {selectedUser
        ? (
          <div className="flex items-center space-x-3 p-2 rounded-md">
            <Avatar img={selectedUser.avatar} rounded />
            <span className="font-medium">{selectedUser.fullName}</span>
            <Button size="lg" color="failure" onClick={handleClear}>
              <FaCircleXmark />
            </Button>
          </div>
        )
        : (
          <div>
            <TextInput
              placeholder="Search user..."
              value={query}
              onChange={(e) => setQuery(e.target.value)}
              onBlur={handleCloseSuggestion}
              disabled={loading}
            />
            {loading && (
              <div className="mt-1">
                <Spinner size="sm" />
              </div>
            )}
            {suggestions.length > 0 && (
              <ul className="absolute z-10 mt-1 w-full bg-white border rounded shadow text-sm max-h-48 overflow-auto">
                {suggestions.map((user, index) => (
                  <li
                    key={index}
                    className="px-3 py-2 hover:bg-gray-100 cursor-pointer"
                    onClick={() => handleSelect(user)}
                  >
                    <div className="flex items-center space-x-2">
                      <Avatar img={user.avatar} size="xs" />
                      <span>{user.fullName}</span>
                    </div>
                  </li>
                ))}
              </ul>
            )}
            {showError && (
              <p className="mt-1 text-sm text-red-600">{meta.error}</p>
            )}
          </div>
        )}
    </div>
  );
};

export default FormUserPicker;
