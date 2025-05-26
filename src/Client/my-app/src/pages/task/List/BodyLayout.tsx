// Copyright (c) 2025 - Jun Dev. All rights reserved

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
} from "../../../components/common";
import { useTaskList } from "./useTaskList";
import { formatDate } from "../../../common/utils/datetime";

const BodyLayout = () => {
  const { data, page, pageSize, onPageSizeChange } = useTaskList();
  if (!data) return <Spinner />;

  return (
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
            {data.items.map(({ title, description, startDate, dueDate, progress, status }, index) => (
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
                    <DropdownItem>View</DropdownItem>
                    <DropdownItem>Edit</DropdownItem>
                    <DropdownItem>Delete</DropdownItem>
                  </Dropdown>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
        <div className="flex items-center justify-between p-4 bg-gray-50 border-t border-gray-200 text-sm text-gray-600">
          <SearchInfo page={page} pageSize={pageSize} searchCount={data.items.length} totalCount={data.totalItems} onPageSizeChange={onPageSizeChange} />
          <Pagination currentPage={1} totalPages={100} onPageChange={() => console.log("abc")} />
        </div>
      </div>
    </div>
  );
};

export default BodyLayout;
