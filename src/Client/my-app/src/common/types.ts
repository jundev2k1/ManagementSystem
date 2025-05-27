// Copyright (c) 2025 - Jun Dev. All rights reserved

export interface SearchResult<T> {
    items: T[],
    totalItems: number,
    totalPages: number,
    pageSize: number,
    pageNumber: number,
}

export interface SearchRequest {
  filters: FilterItem[];
  sorts: SortItem[],
  keyword: string,
  page: number;
  pageSize: number;
}

export interface FilterItem {
  field: string;
  operator: "eq" | "ne" | "gt" | "gte" | "lt" | "lte" | "c" | "sw" | "ew";
  value: any;
}

export interface SortItem {
  field: string;
  direction: "asc" | "desc";
}

export interface Entity {
	createdAt?: string;
	createdBy?: string;
	lastModifiedAt?: string;
	lastModifiedBy?: string;
}