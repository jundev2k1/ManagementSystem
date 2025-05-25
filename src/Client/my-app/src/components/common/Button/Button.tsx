// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Button } from "flowbite-react";

interface ButtonProps {
  children: React.ReactNode;
  onClick?: () => void;
  color?: "blue" | "gray" | "green" | "red" | "yellow";
  type?: "button" | "submit" | "reset";
  isLoading?: boolean;
}

const CommonButton = ({
  children,
  onClick,
  color = "blue",
  type = "button",
  isLoading = false,
}: ButtonProps) => {
  return (
    <Button color={color} type={type} onClick={onClick} disabled={isLoading}>
      {isLoading ? "Loading..." : children}
    </Button>
  );
};

export default CommonButton;