// Copyright (c) 2025 - Jun Dev. All rights reserved

import { Modal } from "../../../components/common";

interface BodyDetailLayoutProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: React.ReactNode;
  isPopup?: boolean;
};

const ModalLayout = ({ isOpen, onClose, title, children, isPopup = false }: BodyDetailLayoutProps) => {
  return (
    <Modal title={title} isOpen={isOpen} onClose={onClose} isPopup={isPopup}>
      {children}
    </Modal>
  );
};

export default ModalLayout;
