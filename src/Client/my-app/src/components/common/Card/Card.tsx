// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Card as FlowbiteCard } from "flowbite-react";

interface CardProps {
  title: string;
  children: React.ReactNode;
}

const Card = ({ title, children }: CardProps) => {
  return (
    <FlowbiteCard className="w-full shadow-lg">
      <h5 className="text-xl font-bold tracking-tight text-gray-900">
        {title}
      </h5>
      {children}
    </FlowbiteCard>
  );
};

export default Card;
