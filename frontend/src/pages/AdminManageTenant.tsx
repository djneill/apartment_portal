import GuestProfileIcon from "../components/guests/GuestProfileIcon";
import LeaseCountdown from "../components/LeaseCountdown";
import { PackageCard } from "../tenantDashboard/components";
import IssuesList from "../components/issues/IssueList";
import CurrentGuestTable from "../components/guests/CurrentGuestTable";
export default function AdminManageTenant() {
  //TODO: package card component needs to have the add package btn if user is admin
  //- latest issues needs to pull data from user

  const guests = ["Josh O", "David O", "Ayo O", "Dennis S"];

  return (
    <div className="px-4 mt-14 font-heading min-h-screen space-y-6">
      <h1 className="text-center text-2xl font-heading md:text-left md:text-3xl">
        Manage Tenant
      </h1>

      <div className="md:flex space-y-6 md:space-x-4">
        <div className="flex space-x-4 items-center md:w-1/2">
          <GuestProfileIcon size={100} iconSize={60} />
          <div className="flex flex-col space-y-1">
            <p className="text-2xl font-medium">Dennis Garcia</p>
            <p className="text-xl">Unit 204</p>
          </div>
        </div>

        <LeaseCountdown />
      </div>

      <PackageCard />

      <IssuesList />

      <CurrentGuestTable activeGuests={guests} />
    </div>
  );
}
