// Copyright (c) 2025 - Jun Dev. All rights reserved

import { format, parseISO, isValid } from "date-fns";

export function formatDate(date: Date | string, pattern = "dd/MM/yyyy HH:mm") {
  const d = typeof date === "string" ? parseISO(date) : date;
  return isValid(d) ? format(d, pattern) : "";
}

export function isDateString(value: any): boolean {
  return typeof value === "string" && !isNaN(Date.parse(value));
}

export function parseDate(value: any): any {
  return isDateString(value) ? new Date(value) : value;
}