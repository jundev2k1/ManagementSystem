// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Form, Formik } from "formik";
import { taskApi, type TaskModel } from "../../../api/services";
import { formatDate } from "../../../common/utils/datetime";
import { Button, Input, toast } from "../../../components/common";
import { ProgressInput, StatusInput } from "../Shared";
import { createTaskRequest, intialTaskValues, validateSchema } from "./shared";
import { UserPicker } from "../../user";

type BodyUpsertTaskProps = {
  onRefreshList: () => void;
  onClose: () => void;
  data?: TaskModel;
  isCreate?: boolean;
};

const BodyUpsertTask = ({ onRefreshList, onClose, data, isCreate = false }: BodyUpsertTaskProps) => {
  const dataInput: TaskModel = {
    ...data!,
    startDate: data && data.startDate ? formatDate(data?.startDate, "yyyy-MM-dd") : '',
    dueDate: data && data.dueDate ? formatDate(data?.dueDate, "yyyy-MM-dd") : '',
    actualStartDate: data && data.actualStartDate ? formatDate(data?.actualStartDate, "yyyy-MM-dd") : '',
    actualEndDate: data && data.actualEndDate ? formatDate(data?.actualEndDate, "yyyy-MM-dd") : '',
  };
  const onSubmit = async (values: TaskModel) => {
    const response = isCreate
      ? await taskApi.create(createTaskRequest(values))
      : await taskApi.update(createTaskRequest(values, false));

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
      initialValues={data ? dataInput : intialTaskValues}
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
              <ProgressInput label="Progress (%)" name="progress" />
            </div>
            <div className="col-span-3">
              <StatusInput label="Status" name="status" />
            </div>
            <div className="col-span-3">
              <Input label="Start Date" name="startDate" type="date" />
            </div>
            <div className="col-span-3">
              <Input label="Due Date" name="dueDate" type="date" />
            </div>
            <div className="col-span-3">
              <Input label="Actual Start Date" name="actualStartDate" type="date" />
            </div>
            <div className="col-span-3">
              <Input label="Actual End Date" name="actualEndDate" type="date" />
            </div>
            <div className="col-span-3">
              <UserPicker label="Assigned To" name="assignedTo" />
            </div>
            <div className="col-span-3">
              <UserPicker label="Assigned By" name="assignedBy" />
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
