import { ReactNode } from "react";

interface CardProps {
    children: ReactNode;
    title?: string;
    icon?: ReactNode;
    className?: string;
    headerClassName?: string;
    bodyClassName?: string;
}

const Card = ({ children, title, icon, className = "", headerClassName = "", bodyClassName = "" }: CardProps) => {
    return (
        <div className={`bg-content-background rounded-lg p-4 ${className}`}>
            {(title || icon) && (
                <div className={`flex justify-between items-center mb-4 ${headerClassName}`}>
                    {title && <h2 className="text-xl font-semibold">{title}</h2>}
                    {icon && <div className="text-purple-300">{icon}</div>}
                </div>
            )}
            <div className={bodyClassName}>{children}</div>
        </div>
    );
};

export default Card;
