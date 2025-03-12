interface NotificationItem {
    message: string;
    action?: string;
}

interface HeroCardProps {
    title: string;
    count?: number;
    notifications: NotificationItem[];
    onActionClick?: (index: number) => void;
    onViewAllClick?: () => void;
    className?: string;
}

const HeroCard = ({
    title,
    count,
    notifications,
    onActionClick,
    onViewAllClick,
    className = '',
}: HeroCardProps) => {
    return (

        <div className={`relative ${className}`}>
            {/* Header Card */}
            <div className="bg-primary text-white h-44 rounded-4xl p-6">
                <div className="flex justify-between items-center">
                    <div className="flex items-center gap-2">
                        <h2 className="text-2xl font-medium">{title}</h2>
                        {count != undefined && (
                            <span className="bg-white text-primary px-3 py-1 rounded-full text-sm font-medium">
                                {count}
                            </span>
                        )}
                    </div>
                    <button
                        onClick={onViewAllClick}
                        className="text-white/90 hover:text-white transition-colors"
                    >
                        View All
                    </button>
                </div>
            </div>

            {/* Notification Card */}
            <div className="bg-secondary rounded-4xl -mt-28 mx-4 overflow-hidden">
                {notifications.map((notification, index) => (
                    <div
                        key={index}
                        className="flex justify-between items-center px-6 py-4 hover:bg-white/5 transition-colors border-b-2 border-background/40"
                    >
                        <span className="text-white/90 text-normal">{notification.message}</span>
                        <button
                            onClick={() => onActionClick?.(index)}
                            className="text-white/95 hover:text-white transition-colors text-normal px-2 py-1 bg-accent rounded-2xl"
                        >
                            {notification.action || 'View'}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default HeroCard;