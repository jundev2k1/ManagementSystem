// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useState } from "react";
import { MdFilterAlt } from "react-icons/md";
import { FaSortAmountDownAlt } from "react-icons/fa";
import { TextInput } from "../../../components/common";
import { TaskPage } from "../List/useTaskList";
import { BadgeButton } from "../../../components/ui";

interface BodyFilterProps {
  filterCount: number;
  sortCount: number;
  keyword: string;
  onKeywordChange: (keyword: string) => void;
  onOpenModal: (targetPage: string) => void;
}

const BodyFilter = ({
  filterCount,
  sortCount,
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

  return (
    <>
      <TextInput
        onKeyUp={onEnterPress}
        onChange={(e) => setInputValue(e.target.value)}
        value={inputValue}
        placeholder="Search..."
      />
      <BadgeButton className="p-3" count={filterCount} onClick={() => onOpenModal(TaskPage.FILTER)}>
        <MdFilterAlt />
      </BadgeButton>
      <BadgeButton className="p-3" count={sortCount} onClick={() => onOpenModal(TaskPage.SORT)}>
        <FaSortAmountDownAlt />
      </BadgeButton>
    </>
  );
};

export default BodyFilter;
