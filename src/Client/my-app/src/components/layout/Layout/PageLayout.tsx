// Copyright (c) 2025 - Jun Dev. All rights reserved

import React, { type ReactNode, type FC } from "react";

type SectionProps = {
  children: ReactNode;
};

const Header: FC<SectionProps> = ({ children }) => (
  <header className="h-16 bg-white shadow z-10 flex items-center px-4">
    {children}
  </header>
);

const Sidebar: FC<SectionProps> = ({ children }) => (
  <aside className="w-64 bg-gray-100 border-r overflow-y-auto">
    {children}
  </aside>
);

const Body: FC<SectionProps> = ({ children }) => (
  <main className="flex-1 bg-gray-50 overflow-y-auto p-4">{children}</main>
);

type LayoutProps = {
  children: ReactNode;
};

const Layout: FC<LayoutProps> & {
  Header: typeof Header;
  Sidebar: typeof Sidebar;
  Body: typeof Body;
} = ({ children }) => {
  const header = React.Children.toArray(children).find(
    (child: any) => child.type === Header
  );
  const sidebar = React.Children.toArray(children).find(
    (child: any) => child.type === Sidebar
  );
  const body = React.Children.toArray(children).find(
    (child: any) => child.type === Body
  );

  return (
    <div className="h-screen flex flex-col">
      {header}

      <div className="flex flex-1 overflow-hidden">
        {sidebar}
        {body}
      </div>
    </div>
  );
};

Layout.Header = Header;
Layout.Sidebar = Sidebar;
Layout.Body = Body;

export default Layout;
