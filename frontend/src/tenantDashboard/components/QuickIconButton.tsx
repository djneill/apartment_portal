import { ButtonHTMLAttributes, ReactNode } from 'react';
import { Link } from 'react-router-dom';

interface QuickIconButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    icon: ReactNode;
    label?: string;
    variant?: 'primary' | 'secondary';
    shape?: 'square' | 'rounded';
    size?: 'sm' | 'md' | 'lg';
    className?: string;
    to: string;
}

const QuickIconButton = ({
    icon,
    label,
    variant = 'primary',
    shape = 'square',
    size = 'md',
    className = '',
    to,
    ...props
}: QuickIconButtonProps) => {
    const baseStyles = 'flex flex-col items-center gap-2 transition-colors duration-200';

    const variantStyles = {
        primary: 'text-white',
        secondary: 'bg-content-background text-black hover:bg-content-background/90',
    };

    const sizeStyles = {
        sm: 'w-12 h-12',
        md: 'w-16 h-16',
        lg: 'w-20 h-20',
    };

    const shapeStyles = {
        square: 'rounded-[20px]',
        rounded: 'rounded-full',
    };

    const buttonStyles = `
    ${sizeStyles[size]}
    ${shapeStyles[shape]}
    bg-primary
    flex items-center justify-center
    hover:bg-primary/90
    cursor-pointer
  `;

    return (
        <Link to={to} className={`${baseStyles} ${variantStyles[variant]} ${className}`}>
            <button
                className={buttonStyles}
                {...props}
            >
                {icon}
            </button>
            {label && (
                <span className="text-xs text-center text-black">{label}</span>
            )}
        </Link>
    );
};

export default QuickIconButton;