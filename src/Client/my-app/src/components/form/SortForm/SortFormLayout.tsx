// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button, Dropdown, DropdownItem } from "flowbite-react";
import { useMemo, useState } from "react";
import { BsInfoCircle } from "react-icons/bs";
import { FaPlus, FaRegTrashCan } from "react-icons/fa6";
import { RiResetLeftFill } from "react-icons/ri";
import type { SortDirection, SortItem } from "../../../common/types";
import { sortDirections, type SortOptions } from "./types";

const displayDirection: Record<SortDirection, string> = {
  asc: "Accending",
  desc: "Descending",
  "": "",
};

type SortFormProps = {
  settings: SortOptions[];
  sorts: SortItem[];
  onSortChange: (filters: SortItem[]) => void;
  onClose: () => void;
};

const SortFormLayout = ({
  settings = [],
  sorts = [],
  onSortChange,
  onClose,
}: SortFormProps) => {
  const defaultSorts: SortItem[] = useMemo(
    () =>
      settings
        .filter((item) => item.isDefault && item.defaultDirection)
        .map((item) => ({
          field: item.field,
          direction: item.defaultDirection!,
        })) || [],
    [],
  );
  const defaultFields = settings.filter(s => s.isDefault).map(df => df.field);
  const initSortRows = [
    ...defaultSorts,
    ...sorts.filter(sort =>
      (defaultSorts.length === 0) || defaultSorts.some(ds => ds.field !== sort.field)
    ),
  ];
  const [filterRows, setFilterRows] = useState<SortItem[]>(initSortRows);

  const isRowDisabled = (row: SortItem) => defaultFields.includes(row.field);

  const getSortOption = (field: string) => settings.find((item) => item.field === field);

  const initDataCount = initSortRows.length;

  const onFieldChange = (fieldValue: string, targetIndex: number) => {
    const targetRow = filterRows[targetIndex];
    targetRow.field = fieldValue;
    setFilterRows([...filterRows]);
  };

  const RenderSelectFieldOptions = (field: string, targetIndex: number) => {
    const isDisabled = (item: SortOptions) => item.field === field || filterRows.some(row => row.field === item.field);
    return (
      <>
        <DropdownItem className="text-gray-400" value="" disabled>
          Select...
        </DropdownItem>
        {settings.map((item, loopIndex) => (
          <DropdownItem
            key={loopIndex}
            className={isDisabled(item) ? "text-gray-400" : undefined}
            value={item.field}
            onClick={() => onFieldChange(item.field, targetIndex)}
            disabled={isDisabled(item)}
          >
            {item.display}
          </DropdownItem>
        ))}
      </>
    );
  };

  const onDirectionChange = (
    fieldValue: SortDirection,
    targetIndex: number,
  ) => {
    const targetRow = filterRows[targetIndex];
    targetRow.direction = fieldValue;
    setFilterRows([...filterRows]);
  };

  const RenderDirectionOptions = (option: SortItem, targetIndex: number) => {
    const isDisabled = (direction: SortDirection) => direction === option.direction;
    return (
      <>
        <DropdownItem className="text-gray-400" value="" disabled>
          Select...
        </DropdownItem>
        {sortDirections.map((directionText, loopIndex) => (
          <DropdownItem
            key={loopIndex}
            className={isDisabled(directionText) ? "text-gray-400" : undefined}
            value={directionText}
            onClick={() => onDirectionChange(directionText, targetIndex)}
            disabled={isDisabled(directionText)}
          >
            {displayDirection[directionText]}
          </DropdownItem>
        ))}
      </>
    );
  };

  const onAddCondition = () => {
    setFilterRows([...filterRows, { field: "", direction: "" }]);
  };

  const onRemoveCondition = (index: number) => {
    filterRows.splice(index, 1);
    setFilterRows([...filterRows]);
  };

  const onApply = () => {
    onSortChange([...filterRows.filter(row => row.field && row.direction)]);
    onClose();
  };

  const onResetForm = () => {
    const isReset = confirm("Are you sure?");
    if (!isReset) return;

    setFilterRows([...defaultSorts]);
  };

  return (
    <div>
      <div className="mb-4 flex justify-end gap-2">
        <Button className="flex gap-2" color="blue" onClick={onAddCondition}>
          <FaPlus /> Add Filter
        </Button>
        <Button className="flex gap-2" color="alternative" onClick={onResetForm}>
          <RiResetLeftFill /> Reset
        </Button>
      </div>
      {filterRows.map((row, index) => (
        <div className={`grid grid-cols-12 gap-4 ${initDataCount === index + 1 ? "mb-5" : "mb-2"}`}>
          <div className="col-span-7">
            <Dropdown
              className="w-full justify-between max-w-lg"
              color="alternative"
              label={getSortOption(row.field)?.display || "Select..."}
              disabled={isRowDisabled(row)}
            >
              {RenderSelectFieldOptions(row.field, index)}
            </Dropdown>
          </div>
          <div className="col-span-4">
            <Dropdown
              className="w-full justify-between max-w-xs"
              color="alternative"
              label={displayDirection[row.direction] || "Select..."}
              disabled={isRowDisabled(row)}
            >
              {RenderDirectionOptions(row, index)}
            </Dropdown>
          </div>
          <div className="col-span-1">
            {!isRowDisabled(row)
              ? (
                <Button
                  className="p-4"
                  color="alternative"
                  onClick={() => onRemoveCondition(index)}
                >
                  <FaRegTrashCan />
                </Button>
              )
              : (
                <Button className="p-4" color="alternative" disabled>
                  <BsInfoCircle />
                </Button>
              )}
          </div>
        </div>
      ))}
      <div className="flex justify-end gap-2 mt-4">
        <Button color="alternative" onClick={onClose}>
          Cancel
        </Button>
        <Button color="blue" onClick={onApply}>
          Apply Sorting
        </Button>
      </div>
    </div>
  );
};

export default SortFormLayout;
