// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button } from "flowbite-react";
import * as React from "react";

type IconButtonWithBadgeProps = {
  children: React.ReactNode;
  count?: number;
  className?: string;
  onClick?: () => void;
  size?: "xs" | "sm" | "md" | "lg";
  color?: string;
};

export const BadgeButton = ({
  children,
  count = 0,
  onClick,
  className,
  size = "md",
  color = "alternative",
}: IconButtonWithBadgeProps) => {
  return (
    <div className="relative inline-block">
      <Button
        size={size}
        color={color}
        onClick={onClick}
        className={`w-10 h-10 relative flex items-center justify-center p-0 ${className}`}
      >
        {children}
      </Button>
      {count > 0 && (
        <span className="absolute top-[-4px] right-[-4px] inline-flex items-center justify-center px-1.5 py-0.5 text-xs font-bold leading-none text-white bg-red-600 rounded-full">
          {count}
        </span>
      )}
    </div>
  );
};
export default BadgeButton;
