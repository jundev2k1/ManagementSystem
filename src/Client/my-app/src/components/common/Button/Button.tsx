// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button } from "flowbite-react";

interface ButtonProps {
  children: React.ReactNode;
  onClick?: () => void;
  color?: "blue" | "gray" | "green" | "red" | "yellow" | "alternative";
  type?: "button" | "submit" | "reset";
  isLoading?: boolean;
  outline?: boolean;
  className?: string;
}

const CommonButton = ({
  children,
  onClick,
  color = "blue",
  type = "button",
  isLoading = false,
  outline = false,
  className,
}: ButtonProps) => {
  return (
    <Button
      className={className}
      color={color}
      type={type}
      onClick={onClick}
      disabled={isLoading}
      outline={outline}
    >
      {isLoading ? "Loading..." : children}
    </Button>
  );
};

export default CommonButton;
