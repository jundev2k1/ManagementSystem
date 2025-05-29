// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button, Dropdown, DropdownItem, TextInput } from "flowbite-react";
import { useMemo, useState } from "react";
import { BsInfoCircle } from "react-icons/bs";
import { FaPlus, FaRegTrashCan } from "react-icons/fa6";
import { RiResetLeftFill } from "react-icons/ri";
import type { FilterItem, FilterOperator } from "../../../common/types";
import { UserPicker } from "../../../pages/user";
import { filterOperator, type FilterOptions } from "./types";

const getOperatorOptions = (source: FilterOptions[], field: string) => {
  const targetOption = source.find((item) => item.field === field);
  if (!targetOption) return [];

  let datasource: FilterOperator[] = [];
  switch (targetOption.type) {
    case "text":
      datasource = filterOperator.TEXT;
      break;

    case "number":
      datasource = filterOperator.NUMBER;
      break;

    case "boolean":
      datasource = filterOperator.BOOLEAN;
      break;

    case "datetime":
      datasource = filterOperator.DATETIME;
      break;

    case "enum":
      datasource = filterOperator.ENUM;
      break;

    case "user":
      datasource = filterOperator.USER;
      break;
  }
  return datasource;
};

const displayOperator: Record<FilterOperator, string> = {
  eq: "Equal to",
  ne: "Not equal to",
  gt: "Greater than",
  gte: "Greater than or equal",
  lt: "Less than",
  lte: "Less than or equal",
  c: "Contains",
  sw: "Starts with",
  ew: "Ends with",
  "": "",
};

type FilterFormProps = {
  settings: FilterOptions[];
  filters: FilterItem[];
  onFilterChange: (filters: FilterItem[]) => void;
  onClose: () => void;
};

const FilterFormLayout = ({
  settings = [],
  filters = [],
  onFilterChange,
  onClose,
}: FilterFormProps) => {
  const defaultFilters: FilterItem[] = useMemo(
    () =>
      settings
        .filter((item) => item.isDefault && item.defaultOperator && item.defaultValue)
        .map((item) => ({
          field: item.field,
          operator: item.defaultOperator!,
          value: item.defaultValue,
        })) || [],
    [],
  );
  const defaultFields = settings.filter(s => s.isDefault).map(df => df.field);
  const initFilterRows = [
    ...defaultFilters,
    ...filters.filter(f =>
      (defaultFilters.length === 0) || defaultFilters.some(dv => (dv.field !== f.field) || dv.value != f.value)
    ),
  ];
  const [filterRows, setFilterRows] = useState<FilterItem[]>(initFilterRows);

  const isRowDisabled = (row: FilterItem) => defaultFields.includes(row.field);

  const getFilterOption = (field: string) => settings.find((item) => item.field === field);

  const initDataCount = initFilterRows.length;

  const onFieldChange = (fieldValue: string, targetIndex: number) => {
    const targetRow = filterRows[targetIndex];
    targetRow.field = fieldValue;
    setFilterRows([...filterRows]);
  };

  const onValueChange = (value: any, targetIndex: number) => {
    const targetRow = filterRows[targetIndex];
    targetRow.value = value;
    setFilterRows([...filterRows]);
  };

  const RenderSelectFieldOptions = (field: string, targetIndex: number) => {
    const isDisabled = (item: FilterOptions) => item.field === field || defaultFields.includes(item.field);
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

  const onOperatorChange = (
    fieldValue: FilterOperator,
    targetIndex: number,
  ) => {
    const targetRow = filterRows[targetIndex];
    targetRow.operator = fieldValue;
    setFilterRows([...filterRows]);
  };

  const RenderOperatorOptions = (option: FilterItem, targetIndex: number) => {
    const datasource = getOperatorOptions(settings!, option.field);

    const isDisabled = (operator: FilterOperator) => operator === option.operator;
    return (
      <>
        <DropdownItem className="text-gray-400" value="" disabled>
          Select...
        </DropdownItem>
        {datasource.map((operatorText, loopIndex) => (
          <DropdownItem
            key={loopIndex}
            className={isDisabled(operatorText) ? "text-gray-400" : undefined}
            value={operatorText}
            onClick={() => onOperatorChange(operatorText, targetIndex)}
            disabled={isDisabled(operatorText)}
          >
            {displayOperator[operatorText]}
          </DropdownItem>
        ))}
      </>
    );
  };

  const RenderValueOptions = (row: FilterItem, targetIndex: number) => {
    const option = getFilterOption(row.field);
    const inputType = option?.type === "text"
      ? "text"
      : option?.type === "datetime"
      ? "date"
      : option?.type === "number"
      ? "number"
      : "";
    return (
      <div>
        {["text", "number", "datetime", ""].includes(option?.type || "") && (
          <TextInput
            type={inputType}
            className="disabled:bg-gray-100"
            value={row.value}
            onChange={(event) => onValueChange(event.target.value, targetIndex)}
            disabled={isRowDisabled(row) || !option?.type}
          />
        )}
        {option?.type === "user" && (
          <UserPicker
            defaultValue={row.value}
            onChangeValue={(value) => onValueChange(value, targetIndex)}
            isView={isRowDisabled(row) || !option?.type}
          />
        )}
      </div>
    );
  };

  const onAddCondition = () => {
    setFilterRows([...filterRows, { field: "", operator: "", value: "" }]);
  };

  const onRemoveCondition = (index: number) => {
    filterRows.splice(index, 1);
    setFilterRows([...filterRows]);
  };

  const onApply = () => {
    onFilterChange([...filterRows.filter(row => row.field && row.operator && row.value)]);
    onClose();
  };

  const onResetForm = () => {
    const isReset = confirm("Are you sure?");
    if (!isReset) return;

    setFilterRows([...filterRows.filter(df => defaultFields.includes(df.field))]);
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
        <div
          key={index}
          className={`grid grid-cols-12 gap-4 ${
            (initDataCount > 0) && (initDataCount === (index + 1)) ? "mb-5" : "mb-2"
          }`}
        >
          <div className="col-span-4">
            <Dropdown
              className="w-full justify-between max-w-xs"
              color="alternative"
              label={getFilterOption(row.field)?.display || "Select..."}
              disabled={isRowDisabled(row)}
            >
              {RenderSelectFieldOptions(row.field, index)}
            </Dropdown>
          </div>
          <div className="col-span-3">
            <Dropdown
              className="w-full justify-between max-w-xs"
              color="alternative"
              label={displayOperator[row.operator] || "Select..."}
              disabled={isRowDisabled(row)}
            >
              {RenderOperatorOptions(row, index)}
            </Dropdown>
          </div>
          <div className="col-span-4">{RenderValueOptions(row, index)}</div>
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
          Apply
        </Button>
      </div>
    </div>
  );
};

export default FilterFormLayout;
