// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { FaPlus } from "react-icons/fa";
import type { TaskModel } from "../../../api/services";
import { formatDate } from "../../../common/utils/datetime";
import {
  Button,
  Dropdown,
  DropdownItem,
  Pagination,
  Progress,
  SearchInfo,
  Spinner,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeadCell,
  TableRow,
} from "../../../components/common";
import { FilterForm, SortForm } from "../../../components/form";
import { BodyUpsertTask, DeleteDialog } from "../Actions";
import BodyDetailTask from "../Actions/BodyDetailTask";
import ModalLayout from "../Modal/ModalLayout";
import { StatusBadge as TaskStatusBadge } from "../Shared";
import BodyFilter from "./FilterActionGroup";
import { filterSettings, getModalName, sortSettings, TaskPage, useTaskList } from "./useTaskList";

const BodyLayout = () => {
  const {
    data,
    onRefreshList,
    page,
    onPageChange,
    pageSize,
    onPageSizeChange,
    keyword,
    onKeywordChange,
    filters,
    onFilterChange,
    sorts,
    onSortChange,
    isLoading,
  } = useTaskList();
  const [isOpen, setIsOpen] = useState(false);
  const [bodyPage, setbodyPage] = useState<string>(TaskPage.BLANK);
  const [targetData, setTargetData] = useState<TaskModel>();

  const onOpenModal = (targetPage: string, targetId = "") => {
    if (targetId) {
      const target = data.items.find(item => item.taskId === targetId);
      setTargetData(target);
    }

    setbodyPage(targetPage);
    setIsOpen(true);
  };
  const onCloseModal = () => {
    setIsOpen(false);
    setTargetData(undefined);
    setbodyPage(TaskPage.BLANK);
  };

  return (
    <>
      <div className="mb-2 flex justify-end">
        <div className="flex gap-2">
          <BodyFilter
            keyword={keyword}
            onKeywordChange={onKeywordChange}
            filterCount={filters.length}
            sortCount={sorts.length}
            onOpenModal={onOpenModal}
          />
          <Button className="flex gap-2 cursor-pointer" color="blue" onClick={() => onOpenModal(TaskPage.CREATE)}>
            <FaPlus /> Create
          </Button>
        </div>
      </div>
      <div className="border border-gray-200 rounded-lg overflow-hidden shadow-sm">
        <div className="overflow-x-auto">
          <Table hoverable>
            <TableHead>
              <TableRow>
                <TableHeadCell className="py-4">Task info</TableHeadCell>
                <TableHeadCell className="w-[25%]">Progress</TableHeadCell>
                <TableHeadCell className="w-[15%] text-center">Status</TableHeadCell>
                <TableHeadCell className="w-[10%]"></TableHeadCell>
              </TableRow>
            </TableHead>
            <TableBody className="divide-y">
              {isLoading
                ? (
                  <TableRow>
                    <TableCell className="text-center py-[32px]" colSpan={4}>
                      <Spinner color="gray" />
                    </TableCell>
                  </TableRow>
                )
                : (data.totalItems === 0)
                ? (
                  <TableRow>
                    <TableCell className="text-center py-[32px]" colSpan={4}>
                      <p className="font-bold text-md">No results found</p>
                    </TableCell>
                  </TableRow>
                )
                : (
                  data.items.map((
                    {
                      taskId,
                      title,
                      description,
                      startDate,
                      dueDate,
                      actualStartDate,
                      actualEndDate,
                      progress,
                      status,
                    },
                    index,
                  ) => (
                    <TableRow key={index} className="bg-white dark:border-gray-700 dark:bg-gray-800">
                      <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                        <h3>{title}</h3>
                        <p>{description}</p>
                      </TableCell>
                      <TableCell className="w-[20%]">
                        <p>Start date: {startDate ? formatDate(startDate) : "Not set"}</p>
                        <p>Due date: {dueDate ? formatDate(dueDate) : "Not set"}</p>
                        <p>Actual start date: {actualStartDate ? formatDate(actualStartDate) : "Not set"}</p>
                        <p>Actual end date: {actualEndDate ? formatDate(actualEndDate) : "Not set"}</p>
                        <Progress className="mt-2" color="indigo" progress={progress} size="lg" labelProgress />
                      </TableCell>
                      <TableCell className="text-center">
                        <TaskStatusBadge status={status} />
                      </TableCell>
                      <TableCell>
                        <Dropdown color="alternative" label="Action" dismissOnClick={false}>
                          <DropdownItem onClick={() => onOpenModal(TaskPage.DETAIL, taskId!)}>View</DropdownItem>
                          <DropdownItem onClick={() => onOpenModal(TaskPage.CREATE, taskId!)}>Copy</DropdownItem>
                          <DropdownItem onClick={() => onOpenModal(TaskPage.EDIT, taskId!)}>Edit</DropdownItem>
                          <DropdownItem onClick={() => onOpenModal(TaskPage.DELETE, taskId!)}>Delete</DropdownItem>
                        </Dropdown>
                      </TableCell>
                    </TableRow>
                  ))
                )}
            </TableBody>
          </Table>
          <div className="flex items-center justify-between p-4 bg-gray-50 border-t border-gray-200 text-sm text-gray-600">
            <SearchInfo
              page={page}
              pageSize={pageSize}
              searchCount={data.items.length}
              totalCount={data.totalItems}
              onPageSizeChange={onPageSizeChange}
            />
            <Pagination currentPage={page} totalPages={!isLoading ? data.totalPages : 1} onPageChange={onPageChange} />
          </div>
        </div>
      </div>
      <ModalLayout
        isOpen={isOpen}
        onClose={onCloseModal}
        title={getModalName(bodyPage)}
        isPopup={bodyPage === TaskPage.DELETE}
      >
        {bodyPage === TaskPage.CREATE
          ? <BodyUpsertTask onRefreshList={onRefreshList} onClose={onCloseModal} data={targetData} isCreate />
          : bodyPage === TaskPage.EDIT
          ? <BodyUpsertTask onRefreshList={onRefreshList} onClose={onCloseModal} data={targetData} />
          : bodyPage === TaskPage.DELETE
          ? <DeleteDialog taskId={targetData?.taskId || ""} onRefreshList={onRefreshList} onClose={onCloseModal} />
          : bodyPage === TaskPage.DETAIL
          ? <BodyDetailTask data={targetData!} />
          : bodyPage === TaskPage.FILTER
          ? (
            <FilterForm
              settings={filterSettings}
              filters={filters}
              onFilterChange={onFilterChange}
              onClose={onCloseModal}
            />
          )
          : bodyPage === TaskPage.SORT
          ? <SortForm settings={sortSettings} sorts={sorts} onSortChange={onSortChange} onClose={onCloseModal} />
          : <></>}
      </ModalLayout>
    </>
  );
};

export default BodyLayout;
