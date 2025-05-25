// Copyright (c) 2025 - Jun Dev. All rights reserved

import type { IconType } from "react-icons";
import { FaTasks } from "react-icons/fa";

export interface MenuItem {
  Icon: IconType;
  title: string;
  url: string;
}

export const menuItems: readonly MenuItem[] = Object.freeze([
  {
    Icon: FaTasks,
    title: "Task Management",
    url: "#",
  },
]);
