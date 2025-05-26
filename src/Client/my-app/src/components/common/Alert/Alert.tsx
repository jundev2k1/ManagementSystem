// src/components/ui/Alert.tsx
import { Alert as FlowbiteAlert } from 'flowbite-react';
import type { FC, ReactNode } from 'react';

type AlertVariant = 'info' | 'success' | 'warning' | 'error';

interface AlertProps {
  variant?: AlertVariant;
  children: ReactNode;
  className?: string;
  onDismiss?: () => void;
}

const variantToColor: Record<AlertVariant, 'info' | 'success' | 'warning' | 'failure'> = {
  info: 'info',
  success: 'success',
  warning: 'warning',
  error: 'failure',
};

const Alert: FC<AlertProps> = ({
  variant = 'info',
  children,
  className = '',
  onDismiss,
}) => {
  return (
    <FlowbiteAlert
      color={variantToColor[variant]}
      withBorderAccent
      onDismiss={onDismiss}
      className={className}
    >
      {children}
    </FlowbiteAlert>
  );
};

export default Alert;
