import { useState, useEffect } from "react";
import {
  HeroCard,
  QuickIconButton,
  CurrentGuest,
  ThermostatCard,
  LockControlModal,
} from "../tenantDashboard/components";
import { getData } from "../services/api";
import { TriangleAlert, UserRoundPlus, Lock, FilePen } from "lucide-react";
import useGlobalContext from "../hooks/useGlobalContext";
import { useNavigate } from "react-router-dom";
import { Packages } from "../Types";
import PackageList from "../tenantDashboard/components/PackageList";

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
  const [packages, setPackages] = useState<Packages[] | null>(null);
  const [unitNumber, setUnitNumber] = useState("");
  const [isLockModalOpen, setIsLockModalOpen] = useState(false);
  const { user, setUser } = useGlobalContext();
  const navigate = useNavigate();

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
              : null,
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
        `notification/latest?userId=${user?.userId}`,
      );
      setNotifications(data);
    })();
  }, [user?.userId]);

  const handleNotificationClick = (index: number) => {
    const notification = notifications[index];
    if (notification.type === "Issue") {
      navigate("/reportissue");
    }
  };
  

  useEffect(() => {
    const fetchPackages = async () => {
      try {
        const response = await getData<Packages[]>(
          `Package/?userId=${user?.userId}&statusId=6`
        );
        setPackages(response);
      } catch (error) {
        console.error("Error fetching package data:", error);
      }
    };
    fetchPackages();
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
    {
      icon: <Lock size={38} />,
      label: "Control Locks",
      to: "",
      onClick: () => setIsLockModalOpen(true),
    },
    { icon: <FilePen size={38} />, label: "Manage Lease", to: "/manageLease" },
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
          onActionClick={handleNotificationClick}
          onViewAllClick={() => console.log("View all clicked")}
        />

        <div className="grid grid-cols-4 gap-4">
          {quickActions.map((action, index) => (
            <QuickIconButton
              key={index}
              icon={action.icon}
              label={action.label}
              to={action.to}
              onClick={action.onClick}
              variant="primary"
            />
          ))}
        </div>

        <CurrentGuest guests={guests} onViewAll={handleViewAllGuests} />

        <div className="grid grid-cols-2 gap-4">
        {packages && (
  <PackageList
    packages={packages}
    userId={Number(user?.userId)}
    unitNumber={user?.unit?.unitNumber}
    unitId={user?.unit?.id}
  />
)}
          <ThermostatCard />
        </div>
      </div>

      <LockControlModal
        isOpen={isLockModalOpen}
        onClose={() => setIsLockModalOpen(false)}
      />
    </div>
  );
};

export default TenantDashboard;
