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
} from "flowbite-react";
import { useTaskList } from "./useTaskList";
import { AiOutlineMore } from "react-icons/ai";
import { formatDate } from "../../../common/utils/datetime";

const BodyLayout = () => {
  const { data } = useTaskList();
  if (!data) return <Spinner />;

  return (
    <div className="overflow-x-auto">
      <Table>
        <TableHead>
          <TableRow>
            <TableHeadCell>Task info</TableHeadCell>
            <TableHeadCell>Progress</TableHeadCell>
            <TableHeadCell>Status</TableHeadCell>
            <TableHeadCell></TableHeadCell>
          </TableRow>
        </TableHead>
        <TableBody className="divide-y">
          {data.items.map(({ title, description, startDate, dueDate, progress, status }, index) => (
            <TableRow key={index} className="bg-white dark:border-gray-700 dark:bg-gray-800">
              <TableCell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                <h3>{title}</h3>
                <p>{description}</p>
              </TableCell>
              <TableCell>
                <p>Start date: {formatDate(startDate)}</p>
                <p>Start date: {formatDate(dueDate)}</p>
                <Progress progress={progress} progressLabelPosition="inside" textLabelPosition="outside" size="lg" labelProgress labelText />
              </TableCell>
              <TableCell>{status}</TableCell>
              <TableCell>
                <Dropdown label="" dismissOnClick={false} renderTrigger={() => <button className="bg-gray-100 p-2 rounded shadow cursor-pointer"><AiOutlineMore /></button>}>
                  <DropdownItem>View</DropdownItem>
                  <DropdownItem>Edit</DropdownItem>
                  <DropdownItem>Delete</DropdownItem>
                </Dropdown>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    <div className="flex overflow-x-auto sm:justify-center">
      <Pagination currentPage={1} totalPages={100} onPageChange={() => console.log("abc")} />
    </div>
    </div>
  );
};

export default BodyLayout;
