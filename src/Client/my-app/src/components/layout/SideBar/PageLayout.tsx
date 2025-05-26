// Copyright (c) 2025 - Jun Dev. All rights reserved

import { useAuth } from "../../../features";
import {
  Avatar,
  Badge,
  Sidebar,
  SidebarCTA,
  SidebarItem,
  SidebarItemGroup,
  SidebarItems,
} from "flowbite-react";
import { menuItems } from "./menu";
import { TbLogout } from "react-icons/tb";

const SidebarMenu = () => {
  const { auth, onLogout } = useAuth();

  const handleLogout = () => {
    onLogout();
  };

  return (
    <Sidebar aria-label="Sidebar with call to action button example">
      <div className="py-2 inline-flex">
        <Avatar placeholderInitials="RR" rounded bordered>
          <p className="font-medium text-lg">
            {`${auth?.firstName || ''} ${auth?.lastName || ''}`}
          </p>
          <span className="text-sm">{auth?.email}</span>
        </Avatar>
      </div>
      <SidebarItems>
        <SidebarItemGroup>
          {menuItems.map(({ Icon, title, url }, index) => (
            <SidebarItem key={index} href={url} icon={Icon} active={title==="Tasks"}>
              {title}
            </SidebarItem>
          ))}

        </SidebarItemGroup>
        <SidebarItemGroup>
          <SidebarItem className="cursor-pointer" onClick={handleLogout} icon={TbLogout}>
            Logout
          </SidebarItem>
        </SidebarItemGroup>
      </SidebarItems>
      <SidebarCTA>
        <div className="mb-3 flex items-center">
          <Badge color="warning">Note</Badge>
        </div>
        <div className="mb-3 text-sm text-cyan-900 dark:text-gray-400">
          The UI is still being developed.
        </div>
      </SidebarCTA>
    </Sidebar>
  );
};

export default SidebarMenu;
