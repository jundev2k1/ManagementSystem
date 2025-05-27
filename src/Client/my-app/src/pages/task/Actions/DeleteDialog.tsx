// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { taskApi } from "../../../api/services/task";
import { Button, toast } from "../../../components/common";
import { HiOutlineExclamationCircle } from "react-icons/hi";

type DeleteDialogProps = {
  taskId: string;
  onRefreshList: () => void;
  onClose: () => void;
};

const DeleteDialog = ({ taskId, onRefreshList, onClose }: DeleteDialogProps) => {
  const [isLoading, setIsLoading] = useState(false);
  const onDeleteItem = () => {
    setIsLoading(true);
    taskApi.delete(taskId).then(() => {
      onRefreshList();
      onClose();
      toast.success('Remove success item.');
    });
  };
  return (
    <div className="text-center">
      <HiOutlineExclamationCircle className="mx-auto mb-4 h-14 w-14 text-gray-400 dark:text-gray-200" />
      <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
      Are you sure you want to delete this task?
      </h3>
      <div className="flex justify-center gap-2">
        <Button color="red" onClick={onDeleteItem} isLoading={isLoading}>Yes, Im Sure</Button>
        <Button color="alternative" onClick={onClose}>Cancel</Button>
      </div>
    </div>
  );
};

export default DeleteDialog;
