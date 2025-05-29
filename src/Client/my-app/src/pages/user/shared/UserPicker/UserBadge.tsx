// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Avatar, Label, Spinner } from "flowbite-react";
import { useEffect, useState } from "react";
import { userApi, type UserModel } from "../../../../api/services";

type UserBadgeProps = {
  label?: string;
  value: string;
};

const UserBadge = ({ label = "", value = "" }: UserBadgeProps) => {
  const [selectedUser, setSelectedUser] = useState<UserModel | null>(null);
  const [loading, setLoading] = useState(false);

  // Init fetch user
  useEffect(() => {
    if (!selectedUser && value) {
      setLoading(true);
      userApi.getById(value)
        .then((res) => setSelectedUser(res.data))
        .finally(() => setLoading(false));
    }
  }, [value]);

  return (
    <>
      {label && <Label>{label}</Label>}
      {loading && <Spinner />}
      {!loading && selectedUser
        ? (
          <div className="flex items-center space-x-3 p-2 rounded-md">
            <Avatar img={selectedUser.avatar} rounded />
            <span className="font-medium">{selectedUser.fullName}</span>
          </div>
        )
        : <p className="text-gray-400">Not set</p>}
    </>
  );
};

export default UserBadge;
