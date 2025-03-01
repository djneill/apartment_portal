import { ButtonHTMLAttributes, ReactNode } from "react";

interface MainButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    children: ReactNode;
    variant?: 'primary' | 'secondary' | 'accent' | 'background' | 'content-background' | 'icon';
    fullWidth?: boolean;
    icon?: ReactNode;
    iconPosition?: 'left' | 'right';
    size?: 'sm' | 'md' | 'lg';
    className?: string;
    noHoverTransform?: boolean;
}

const MainButton = ({
    children,
    variant = 'primary',
    fullWidth = false,
    icon,
    iconPosition = 'left',
    size = 'md',
    className = "",
    noHoverTransform = false,
    ...props
}: MainButtonProps) => {
    return (
        <button
            className={`rounded transition-all duration-200 flex items-center justify-center mx-auto
            ${variant === 'primary' ? 'bg-primary hover:bg-primary/80 text-white' : ''}
            ${variant === 'secondary' ? 'bg-secondary hover:bg-secondary/80 text-primary' : ''}
            ${variant === 'accent' ? 'bg-[#C4AEF1] hover:bg-[#C4AEF1]/80 text-white' : ''}
            ${variant === 'background' ? 'bg-background hover:bg-background/80 text-white ' : ''}
            ${variant === 'content-background' ? 'bg-content-background hover:bg-content-background/80 text-white ' : ''}
            ${variant === 'icon' ? 'bg-accent hover:bg-accent/80 text-white rounded-full p-2' : ''}
            ${fullWidth ? 'w-full' : ''}
            ${noHoverTransform ? 'hover:transform-none' : ''}
            ${variant != icon ? (size === 'sm' ? 'text-sm py-1 px-3' : size === 'md' ? 'py-2 px-4' : size === 'lg' ? 'text-lg py-3 px-6' : '') : ''}
            ${className}
            `}
            {...props}
        >
            {icon && iconPosition === 'left' && <span className={children ? 'mr-2' : ''}>{icon}</span>}
            {children}
            {icon && iconPosition === 'right' && <span className={children ? 'ml-2' : ''}>{icon}</span>}
        </button>
    );
};

export default MainButton;