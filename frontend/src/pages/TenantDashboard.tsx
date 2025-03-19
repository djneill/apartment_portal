import { useState, useEffect } from "react";
import {
  HeroCard,
  QuickIconButton,
  CurrentGuest,
  PackageCard,
  ThermostatCard,
} from "../tenantDashboard/components";
import { getData } from "../services/api";
import { TriangleAlert, UserRoundPlus, Lock, FilePen } from "lucide-react";
import useGlobalContext from "../hooks/useGlobalContext";

type Notifications = {
  date: string;
  message: string;
  type: string;
};

type UserWithUnit = {
  user: {
    id: string;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    statusId: number;
  };
  unit: {
    id: number;
    unitNumber: string;
    price: number;
    statusName: null | string;
  };
};

const TenantDashboard = () => {
  const [notifications, setNotifications] = useState<Notifications[]>([]);
  const [packageCount, setPackageCount] = useState(0);
  const [unitNumber, setUnitNumber] = useState("");
  const { user, setUser } = useGlobalContext();

  useEffect(() => {
    const fetchUserDetails = async () => {
      if (user?.userId) {
        try {
          const userData = await getData<UserWithUnit>(`Users/${user.userId}`);
          if (userData.unit?.unitNumber) {
            setUnitNumber(userData.unit.unitNumber);
          }

          setUser((prev) =>
            prev
              ? {
                ...prev,
                unit: userData.unit,
              }
              : null
          );
        } catch (error) {
          console.error("Error fetching user details:", error);
        }
      }
    };

    fetchUserDetails();
  }, [user?.userId, setUser]);

  useEffect(() => {
    (async () => {
      const data = await getData<Notifications[]>(
        `notification/latest?userId=${user?.userId}`
      );
      setNotifications(data);

      const packagesAvailable = data.filter((notification) =>
        notification.message.toLowerCase().includes("package")
      ).length;

      setPackageCount(packagesAvailable);
    })();
  }, [user?.userId]);

  const quickActions = [
    {
      icon: <TriangleAlert size={38} />,
      label: "Report Issues",
      to: "/reportissue",
    },
    {
      icon: <UserRoundPlus size={38} />,
      label: "Manage Guests",
      to: "/guests",
    },
    { icon: <Lock size={38} />, label: "Control Locks", to: "/" },
    { icon: <FilePen size={38} />, label: "Manage Lease", to: "/" },
  ];

  const guests = ["Dennis G.", "David O.", "Felipe A."];
  const handleViewAllGuests = () => {
    console.log("View all guests");
  };

  return (
    <div className="min-h-screen bg-background">
      {/* TODO: Create header component */}
      <header className="px-4 pt-15 pb-1 flex flex-col justify-between items-center">
        <div className="flex justify-between w-full">
          <h1 className="font-medium">Welcome, {user?.firstName}</h1>
          <p className="font-medium">Unit {unitNumber}</p>
        </div>
      </header>

      <div className="p-4 space-y-6">
        <HeroCard
          title="Notifications"
          count={notifications.length}
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

        <CurrentGuest guests={guests} onViewAll={handleViewAllGuests} />

        <div className="grid grid-cols-2 gap-4">
          <PackageCard packageCount={packageCount} />
          <ThermostatCard />
        </div>
      </div>
    </div>
  );
};

export default TenantDashboard;
