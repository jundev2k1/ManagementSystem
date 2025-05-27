// Copyright (c) 2025 - Jun Dev. All rights reserved

import {
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter
} from "flowbite-react";

interface CommonModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  size?: "sm" | "md" | "lg" | "xl" | "2xl" | "3xl" | "4xl" | "5xl" | "6xl" | "7xl";
  children: React.ReactNode;
  footer?: React.ReactNode;
  isPopup?: boolean;
}

const CommonModal = ({
  isOpen,
  onClose,
  title,
  size = "4xl",
  children,
  footer,
  isPopup = false,
}: CommonModalProps) => {
  return (
    <Modal show={isOpen} onClose={onClose} size={size} position="center" popup={isPopup}>
      <ModalHeader>{title}</ModalHeader>
      <ModalBody>{children}</ModalBody>
      {footer && <ModalFooter>{footer}</ModalFooter>}
    </Modal>
  );
};
export default CommonModal;
