// Copyright (c) 2025 - Jun Dev. All rights reserved

import {
  addDays,
  addMonths,
  endOfDay,
  format,
  isAfter,
  isBefore,
  isSameDay,
  isValid,
  parseISO,
  startOfDay,
  subDays,
  subMonths,
} from "date-fns";

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

export const getToday = () => new Date();

export const getTomorrow = () => addDays(new Date(), 1);

export const getYesterday = () => subDays(new Date(), 1);

export const getNDaysFromNow = (n: number) => addDays(new Date(), n);

export const getNDaysAgo = (n: number) => subDays(new Date(), n);

export const getNMonthsFromNow = (n: number) => addMonths(new Date(), n);

export const getNMonthsAgo = (n: number) => subMonths(new Date(), n);

export const startOfToday = () => startOfDay(new Date());

export const endOfToday = () => endOfDay(new Date());

export const isToday = (date: Date) => isSameDay(date, new Date());

export const isDateAfterToday = (date: Date) => isAfter(date, new Date());

export const isDateBeforeToday = (date: Date) => isBefore(date, new Date());
