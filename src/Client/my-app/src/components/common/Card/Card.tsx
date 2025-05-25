// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Card } from "flowbite-react";

interface CommonCardProps {
  title: string;
  children: React.ReactNode;
}

export const CommonCard = ({ title, children }: CommonCardProps) => {
  return (
    <Card className="w-full shadow-lg">
      <h5 className="text-xl font-bold tracking-tight text-gray-900">{title}</h5>
      {children}
    </Card>
  );
};