import CurrentGuestTable from "../components/guests/CurrentGuestTable";
import GuestForm from "../components/guests/GuestForm";
import PreviousGuests from "../components/guests/PreviousGuests";

export default function ManageGuests() {
  //TODO:
  //  - connect api for submit and pull current guest list
  //  - send info to components
  //  - desktop version

  function handleSubmit(data: {
    fullName: string;
    phoneNumber: string;
    duration: string;
    carMakeModel?: string;
    carColor?: string;
    licensePlate?: string;
  }) {
    console.log(data)
  }
  return (
    <div className="w-screen min-h-screen p-4">
      <h1 className="mt-12 mb-4 font-heading font-medium text-3xl">Manage Guests</h1>
      <div className="flex flex-col items-center ">
        <div className="w-full space-y-10 md:flex  md:space-x-10">
          <div className="flex-none w-full md:w-2/3 md:mt-10">
            <GuestForm onSubmit={handleSubmit} />
          </div>
          <div className="md:flex flex-col flex-grow space-y-8">
            <CurrentGuestTable />
            <PreviousGuests />
          </div>
        </div>
      </div>
    </div>
  )
}
