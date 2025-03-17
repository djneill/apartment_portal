import { TriangleAlert, UserRoundPlus, Lock, FilePen, ArrowRight } from "lucide-react";
import HeroCard from "../tenantDashboard/components/HeroCard";
import { QuickIconButton } from "../tenantDashboard/components";
import { InsightLogo } from "../assets/InsightLogo";
import IssuesList from "../components/issues/IssueList";

export default function AdminDashboard() {

  const notifications: [] = []

  const insightCards = [
    { title: "Recurring Issue Detected", description: "Leak reported in Apt 302 for the third time. dfailsdjf lalsdjflasjdl flasjdf" },
    { title: "Recurring Issue Detected", description: "Leak reported in Apt 302 for the third time." },
    { title: "Recurring Issue Detected", description: "Leak reported in Apt 302 for the third time." },
    { title: "Recurring Issue Detected", description: "Leak reported in Apt 302 for the third time." },
  ]

  const renderInsights = insightCards.map((insight, index) => {
    return (
      <div
        key={index}
        className="w-72 bg-white p-4 rounded-2xl flex flex-col whitespace-nowrap "
        style={{ boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)' }}
      >
        <p className="font-semibold mb-1">{insight.title}</p>
        <div className="flex flex-col">
          <p className="font-light text-sm line-clamp-2">
            {insight.description}
          </p>
          <div className=" cursor-pointer text-sm flex items-center space-x-1">
            <p className="text-accent">Show suggestions</p>
            <ArrowRight size={12} color="#C4AEF1" />
          </div>
        </div>
      </div>
    );
  });

  //TODO: change to appropriate routes
  const quickActions = [
    { icon: <TriangleAlert size={38} />, label: "Report Issues", to: "/reportissue" },
    { icon: <UserRoundPlus size={38} />, label: "Register Tenant", to: "/" },
    { icon: <Lock size={38} />, label: "Security", to: "/" },
    { icon: <FilePen size={38} />, label: "Manage Lease", to: "/" },
  ];

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

        <div className="-mr-5">
          <div className="w-full flex justify-between mb-4 font-heading pr-5">
            <div className="flex space-x-1 items-center"><InsightLogo /><p className="text-md font-semibold text-dark-gray">Insights</p></div>
            <p className="text-primary font-semibold text-sm">View all</p>
          </div>

          <div className="flex w-full overflow-scroll space-x-3 py-2 ">
            {renderInsights}
          </div>
        </div>

        <IssuesList />

      </div>

    </div>
  )
}
