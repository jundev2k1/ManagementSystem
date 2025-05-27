// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { taskApi } from "../../../api/services/task";
import { Button } from "../../../components/common";

type DeleteDialogProps = {
  taskId: string;
  onClose: () => void;
};

const DeleteDialog = ({ taskId, onClose }: DeleteDialogProps) => {
  const [isLoading, setIsLoading] = useState(false);
  const onDeleteItem = () => {
    setIsLoading(true);
    debugger
    taskApi.delete(taskId).then(() => onClose());
  };
  return (
    <>
      <p className="mb-4">
        This action cannot be undone. Are you sure you want to delete this item?
      </p>
      <div className="flex items-center gap-2">
        <Button color="red" onClick={onDeleteItem} isLoading={isLoading}>Delete</Button>
        <Button color="alternative" onClick={onClose}>Cancel</Button>
      </div>
    </>
  );
};

export default DeleteDialog;
