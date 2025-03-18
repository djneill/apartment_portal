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
      <div className="bg-primary text-white h-32 rounded-3xl p-4 mb-10">
        <div className="flex justify-between items-center">
          <div className="flex items-center gap-2  font-heading">
            <h2 className="text-xl font-medium">{title}</h2>
            {count != undefined && (
              <span className="bg-white text-primary px-3 py-1 rounded-2xl text-sm font-medium">
                {count}
              </span>
            )}
          </div>
          <button
            onClick={onViewAllClick}
            className="text-white/90 hover:text-white transition-colors text-lg cursor-pointer"
          >
            View All
          </button>
        </div>
      </div>

      {/* Notification Card */}
      <div className="bg-secondary rounded-3xl -mt-28 mx-4 overflow-hidden">
        {notifications.length > 0 ? (
          notifications.map((notification, index) => (
            <div
              key={index}
              className="flex justify-between items-center px-6 py-4 hover:bg-white/5 transition-colors border-b-2 border-background/40"
            >
              <span className="text-white/90 text-normal">{notification.message}</span>
              <button
                onClick={() => onActionClick?.(index)}
                className="text-white hover:text-white transition-colors text-normal px-2 py-1 bg-accent rounded-2xl"
              >
                {notification.action || 'View'}
              </button>
            </div>
          ))
        ) : (
          <div className="px-6 py-8 text-center h-40">
            <p className="text-white text-xl font-body">No new notifications, have a great day! 🎉</p>
          </div>
        )}
      </div>
    </div>
  );
};

export default HeroCard;
