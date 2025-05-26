// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Select } from "flowbite-react";

const validPageSizes = Object.freeze([5, 10, 20, 50, 100]);

type SearchInfoProps = {
  page: number;
  pageSize: number;
  searchCount: number;
  totalCount: number;
  onPageSizeChange: (pageSize: number) => void;
};

const SearchInfo = ({
  page,
  pageSize,
  searchCount,
  totalCount,
  onPageSizeChange,
}: SearchInfoProps) => {
  const showFrom = (page - 1) * pageSize;
  const showTo = showFrom + searchCount;
  return (
    <div className="flex items-center gap-2">
      <span>
        Showing {showFrom}–{showTo} of {totalCount} results
      </span>
      <span className="text-gray-400">|</span>
      <div className="flex items-center gap-2">
        <span className="whitespace-nowrap">
          Items per page:
        </span>
        <Select
          sizing="sm"
          value={pageSize}
          onChange={(e) => onPageSizeChange(Number(e.target.value))}
        >
          {validPageSizes.map((size, index) => (
            <option key={index} value={size}>
              {size}
            </option>
          ))}
        </Select>
      </div>
    </div>
  );
};

export default SearchInfo;
