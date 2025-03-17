import { TriangleAlert, UserRoundPlus, Lock, FilePen } from "lucide-react";
import HeroCard from "../tenantDashboard/components/HeroCard";
import { CurrentGuest, QuickIconButton } from "../tenantDashboard/components";

export default function AdminDashboard() {

  const notifications: [] = []

  const guests = ["Dennis G.", "David O.", "Felipe A."];

  const quickActions = [
    { icon: <TriangleAlert size={38} />, label: "Report Issues", to: "/reportissue" },
    { icon: <UserRoundPlus size={38} />, label: "Manage Guests", to: "/guests" },
    { icon: <Lock size={38} />, label: "Control Locks", to: "/" },
    { icon: <FilePen size={38} />, label: "Manage Lease", to: "/" },
  ];


  //TODO: implement functions ref manage guests 
  const handleAddGuest = () => {
    console.log("Add new guest");
  };
  const handleViewAllGuests = () => {
    console.log("View all guests");
  };

  return (
    <div className="min-h-screen p-5 md:p-10">

      <div className="space-y-6 mt-14 ">
        <header className="">
          <h1 className="font-normal text-2xl font-heading">Welcome John Doe</h1>
        </header>
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


      </div>

    </div>
  )
}
