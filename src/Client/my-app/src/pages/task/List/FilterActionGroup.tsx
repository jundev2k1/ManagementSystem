// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { MdFilterAlt } from "react-icons/md";
import { FaSortAmountDownAlt } from "react-icons/fa";
import { Button, TextInput } from "../../../components/common";
import type { FilterItem, SortItem } from "../../../common/types";
import { TaskPage } from "../List/useTaskList";

interface BodyFilterProps {
  filters: FilterItem[];
  onFilterChange: (filters: FilterItem[]) => void;
  sorts: SortItem[];
  onSortChange: (sorts: SortItem[]) => void;
  keyword: string;
  onKeywordChange: (keyword: string) => void;
  onOpenModal: (targetPage: string) => void;
}

const BodyFilter = ({
  filters,
  onFilterChange,
  sorts,
  onSortChange,
  keyword,
  onKeywordChange,
  onOpenModal,
}: BodyFilterProps) => {
  const [inputValue, setInputValue] = useState(keyword);

  const onEnterPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.code !== "Enter") return;

    const input = event.target as HTMLInputElement;
    onKeywordChange(input.value);
  };

  console.log(filters);
  console.log(onFilterChange);
  console.log(sorts);
  console.log(onSortChange);

  return (
    <>
      <TextInput
        onKeyUp={onEnterPress}
        onChange={(e) => setInputValue(e.target.value)}
        value={inputValue}
        placeholder="Search..."
      />
      <Button
        className="p-3"
        color="alternative"
        onClick={() => onOpenModal(TaskPage.FILTER)}
      >
        <MdFilterAlt />
      </Button>
      <Button
        className="p-3"
        color="alternative"
        onClick={() => onOpenModal(TaskPage.SORT)}
      >
        <FaSortAmountDownAlt />
      </Button>
    </>
  );
};

export default BodyFilter;
