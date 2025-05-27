// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { FaPlus } from "react-icons/fa";
import { formatDate } from "../../../common/utils/datetime";
import {
  Dropdown,
  DropdownItem,
  Pagination,
  Progress,
  Spinner,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeadCell,
  TableRow,
  SearchInfo,
  Button,
} from "../../../components/common";
import { TaskPage, getModalName, useTaskList } from "./useTaskList";
import BodyFilter from "./FilterActionGroup";
import ModalLayout from "../Modal/ModalLayout";
import { BodyUpsertTask, DeleteDialog} from "../Actions";
import type { TaskModel } from "../../../api/services/task";

const BodyLayout = () => {
  const {
    data,
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

  const onOpenModal = (targetPage: string, targetId = '') => {
    if (targetId) {
      const target = data.items.find(item => item.taskId === targetId);
      setTargetData(target);
    }
    
    setbodyPage(targetPage);
    setIsOpen(true);
  }
  const onCloseModal = () => {
    setIsOpen(false);
    setTargetData(undefined);
    setbodyPage(TaskPage.BLANK);
  }

  return (
    <>
      <div className="mb-2 flex justify-end">
        <div className="flex gap-2">
          <BodyFilter
            keyword={keyword}
            onKeywordChange={onKeywordChange}
            filters={filters}
            onFilterChange={onFilterChange}
            sorts={sorts}
            onSortChange={onSortChange}
            onOpenModal={onOpenModal} />
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
                <TableHeadCell className="w-[15%]">Status</TableHeadCell>
                <TableHeadCell className="w-[10%]"></TableHeadCell>
              </TableRow>
            </TableHead>
            <TableBody className="divide-y">
              { isLoading ? (
                <TableRow>
                  <TableCell className="text-center py-[32px]" colSpan={4}>
                    <Spinner color="gray" />
                  </TableCell>
                </TableRow>
              ) : (
                data.items.map(({ taskId, title, description, startDate, dueDate, progress, status }, index) => (
                  <TableRow key={index} className="bg-white dark:border-gray-700 dark:bg-gray-800">
                    <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                      <h3>{title}</h3>
                      <p>{description}</p>
                    </TableCell>
                    <TableCell className="w-[20%]">
                      <p>Start date: {formatDate(startDate)}</p>
                      <p>Start date: {formatDate(dueDate)}</p>
                      <Progress className="mt-2" color="indigo" progress={progress} size="lg" labelProgress />
                    </TableCell>
                    <TableCell>{status}</TableCell>
                    <TableCell>
                      <Dropdown color="alternative" label="Action" dismissOnClick={false}>
                        <DropdownItem onClick={() => onOpenModal(TaskPage.DETAIL, taskId)}>View</DropdownItem>
                        <DropdownItem onClick={() => onOpenModal(TaskPage.EDIT, taskId)}>Edit</DropdownItem>
                        <DropdownItem onClick={() => onOpenModal(TaskPage.DELETE, taskId)}>Delete</DropdownItem>
                      </Dropdown>
                    </TableCell>
                  </TableRow>
                )))
              }
            </TableBody>
          </Table>
          <div className="flex items-center justify-between p-4 bg-gray-50 border-t border-gray-200 text-sm text-gray-600">
            <SearchInfo page={page} pageSize={pageSize} searchCount={data.items.length} totalCount={data.totalItems} onPageSizeChange={onPageSizeChange} />
            <Pagination currentPage={page} totalPages={!isLoading ? data.totalPages : 1} onPageChange={onPageChange} />
          </div>
        </div>
      </div>
      <ModalLayout isOpen={isOpen} onClose={onCloseModal} title={getModalName(bodyPage)}>
        {bodyPage === TaskPage.CREATE ? (
            <BodyUpsertTask onClose={onCloseModal} isCreate />
          ) : bodyPage === TaskPage.EDIT ? (
            <BodyUpsertTask onClose={onCloseModal} data={targetData} />
          ) : bodyPage === TaskPage.DELETE ? (
            <DeleteDialog taskId={targetData?.taskId || ''} onClose={onCloseModal} />
          ) : bodyPage === TaskPage.DETAIL ? (
            <>Detail Page</>
          ) : bodyPage === TaskPage.FILTER ? (
            <>Filter advanced Page</>
          ) : bodyPage === TaskPage.SORT ? (
            <>Sort advanced Page</>
          ) : (
            <></>
          )}
        
      </ModalLayout>
    </>
  );
};

export default BodyLayout;
