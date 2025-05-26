// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { IconType } from "react-icons";
import { FaTasks } from "react-icons/fa";
import { HiChartPie, HiUser } from "react-icons/hi";

export interface MenuItem {
  Icon: IconType;
  title: string;
  url: string;
}

export const menuItems: readonly MenuItem[] = Object.freeze([
  {
    Icon: HiChartPie,
    title: "Dashboard",
    url: "#",
  },
  {
    Icon: FaTasks,
    title: "Tasks",
    url: "#",
  },
  {
    Icon: HiUser,
    title: "Users",
    url: "#",
  },
]);
