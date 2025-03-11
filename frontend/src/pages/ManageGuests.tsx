import CurrentGuestTable from "../components/guests/CurrentGuestTable";
import GuestForm from "../components/guests/GuestForm";

export default function ManageGuests() {
  //TODO:
  //  - connect api for submit and pull current guest list
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
        <div className="w-full space-y-10">
          <GuestForm onSubmit={handleSubmit} />
          <CurrentGuestTable />
        </div>
      </div>
    </div>
  )
}
