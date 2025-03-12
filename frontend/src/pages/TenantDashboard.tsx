import { useState, useEffect } from "react";
import {
  HeroCard,
  QuickIconButton,
} from "../tenantDashboard/components";
import { getData } from "../services/api";
import { TriangleAlert, UserRoundPlus, Lock, FilePen } from 'lucide-react'
type Notifications = {
  date: string;
  message: string;
  type: string;
};

const TenantDashboard = () => {
  const [notifications, setNotifications] = useState<Notifications[]>([]);

  useEffect(() => {
    (async () => {
      const data = await getData<Notifications[]>("notifications/latest/6");
      setNotifications(data);
    })();
  }, []);

  const quickActions = [
    { icon: <TriangleAlert size={24} />, label: 'Report Issues', to: '/issues' },
    { icon: <UserRoundPlus size={24} />, label: 'Manage Guests', to: 'guests' },
    { icon: <Lock size={24} />, label: 'Control Locks', to: '/lock' },
    { icon: <FilePen size={24} />, label: 'Manage Lease', to: 'lease' },
  ];

  return (
    <div className="min-h-screen bg-background">
      {/* TODO: Create header component */}
      <header className="p-4 flex justify-between items-center">
        <button className="p-2">
          <div className="space-y-1">
            <div className="w-6 h-0.5 bg-primary"></div>
            <div className="w-6 h-0.5 bg-primary"></div>
            <div className="w-6 h-0.5 bg-primary"></div>
          </div>
        </button>
        <div className="flex justify-between w-full">
          <h1 className="text-2xl font-bold">Welcome John</h1>
          <p className="text-secondary">Unit 205</p>
        </div>
      </header>

      <div className="p-4 space-y-6">
        <HeroCard
          title="Notifications"
          count={3}
          notifications={notifications}
          onActionClick={(index) => console.log("Clicked notification", index)}
          onViewAllClick={() => console.log("View all clicked")}
        />

        <div className="grid grid-cols-4 gap-4">
          {quickActions.map((action, index) => (
            <QuickIconButton
              key={index}
              icon={action.icon}
              label={action.label}
              to={action.to}
              variant="primary"
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default TenantDashboard;
