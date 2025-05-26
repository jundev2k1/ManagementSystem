// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button } from "flowbite-react";

interface ButtonProps {
  children: React.ReactNode;
  onClick?: () => void;
  color?: "blue" | "gray" | "green" | "red" | "yellow";
  type?: "button" | "submit" | "reset";
  isLoading?: boolean;
  outline?: boolean;
}

const CommonButton = ({
  children,
  onClick,
  color = "blue",
  type = "button",
  isLoading = false,
  outline = false,
}: ButtonProps) => {
  return (
    <Button
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
