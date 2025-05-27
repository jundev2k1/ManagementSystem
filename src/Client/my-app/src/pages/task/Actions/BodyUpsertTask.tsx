// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Form, Formik } from "formik";
import { Button, Input, toast } from "../../../components/common";
import { taskApi, type TaskModel } from "../../../api/services/task";
import { intialTaskValues, validateSchema } from "./shared";
import { formatDate } from "../../../common/utils/datetime";

type BodyUpsertTaskProps = {
  onRefreshList: () => void;
  onClose: () => void;
  data?: TaskModel;
  isCreate?: boolean;
}

const BodyUpsertTask = ({ onRefreshList, onClose, data, isCreate = false }: BodyUpsertTaskProps) => {
  const dataInput: TaskModel = {
    ...data!,
    startDate: data ? formatDate(data?.startDate, 'yyyy-MM-dd') : '',
    dueDate: data ? formatDate(data?.dueDate, 'yyyy-MM-dd') : '',
  };
  const onSubmit = async (values: TaskModel) => {
    const response = isCreate
      ? await taskApi.create({ ...values, taskId: null })
      : await taskApi.update(values);

    if (!response.isSuccess) {
      toast.error(`${response.message}`);
      return;
    }

    onClose();
    onRefreshList();
    toast.success(`${response.message}`);
  };

  return (
    <Formik
      initialValues={!isCreate ? dataInput || intialTaskValues : intialTaskValues}
      validationSchema={validateSchema}
      onSubmit={onSubmit}
    >
      {({ isSubmitting }) => (
        <Form className="mx-auto px-4 max-h-[75vh] overflow-y-auto">
          <div className="grid grid-cols-6 gap-2">
            <div className="col-span-6">
              <Input label="Title" name="title" />
            </div>
            <div className="col-span-6">
              <Input label="Description" name="description" />
            </div>
            <div className="col-span-3">
              <Input label="Progress (%)" name="progress" type="number" />
            </div>
            <div className="col-span-3">
              <Input label="Status" name="status" type="number" />
            </div>
            <div className="col-span-3">
              <Input label="Start Date" name="startDate" type="date" />
            </div>
            <div className="col-span-3">
              <Input label="Due Date" name="dueDate" type="date" />
            </div>
            <div className="col-span-3">
              <Input label="Assigned To" name="assignedTo" />
            </div>
            <div className="col-span-3">
              <Input label="Assigned By" name="assignedBy" />
            </div>
            <div className="col-span-6">
              <Input label="Note" name="note" />
            </div>
          </div>
          <div className="flex justify-center gap-3 mt-4">
            <Button type="submit" isLoading={isSubmitting}>
              Submit
            </Button>
            <Button type="button" color="alternative" onClick={onClose}>
              Cancel
            </Button>
          </div>
        </Form>
      )}
    </Formik>
  );
};

export default BodyUpsertTask;
