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
  children: React.ReactNode;
  footer?: React.ReactNode;
}

const CommonModal = ({
  isOpen,
  onClose,
  title,
  children,
  footer,
}: CommonModalProps) => {
  return (
    <Modal show={isOpen} onClose={onClose}>
      <ModalHeader>{title}</ModalHeader>
      <ModalBody>{children}</ModalBody>
      {footer && <ModalFooter>{footer}</ModalFooter>}
    </Modal>
  );
};
export default CommonModal;
