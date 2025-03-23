import { useEffect, useState } from "react";
import CurrentGuestTable from "../components/guests/CurrentGuestTable";
import GuestForm from "../components/guests/GuestForm";
import PreviousGuests from "../components/guests/PreviousGuests";
import { getData, postData, patchData } from "../services/api";
import { Guest, GuestRequest } from "../types";
import useGlobalContext from "../hooks/useGlobalContext";

export default function ManageGuests() {
  const [guests, setGuests] = useState<{
    activeGuests: Guest[];
    inactiveGuests: Guest[];
  } | null>(null);
  const [editGuest, setEditGuest] = useState<Guest | null>(null);

  const { user } = useGlobalContext();

  useEffect(() => {
    const fetchGuests = async () => {
      if (!user?.userId) {
        console.error("User not logged in");
        return;
      }
      try {
        const response = await getData<Guest[]>(
          `/Guest?userId=${user?.userId}`,
        );

        const activeGuests = response.filter(
          (guest) => guest.expiration > new Date().toISOString(),
        );
        const inactiveGuests = response.filter(
          (guest) => guest.expiration <= new Date().toISOString(),
        );
        setGuests({ activeGuests, inactiveGuests });
      } catch (error) {
        console.error("Error fetching guests:", error);
      }
    };
    fetchGuests();
  }, [user?.userId]);

  async function handleSubmit(data: GuestRequest) {
    if (!user?.userId) {
      console.error("User is not logged in, cannot submit guest");
      return;
    }
    try {
      if (editGuest) {
        await patchData(`/guest/${editGuest.id}`, {
          ...data,
          id: editGuest.id,
          userId: user?.userId,
        });

        const updatedGuests = await getData<Guest[]>(
          `/Guest?userId=${user?.userId}`,
        );

        const activeGuests = updatedGuests.filter(
          (guest) => new Date(guest.expiration) > new Date(),
        );
        const inactiveGuests = updatedGuests.filter(
          (guest) => new Date(guest.expiration) <= new Date(),
        );

        setGuests({ activeGuests, inactiveGuests });
      } else {
        const newGuest = await postData<Guest>("/guest/register-guest", {
          ...data,
          userId: user?.userId,
        });
        const response = await getData<Guest[]>(
          `/Guest?userId=${user?.userId}`,
        );
        console.log("API Response:", response);

        if (!response) {
          console.error("Error: API response does not contain valid data.");
          return;
        }
        const activeGuests = response.filter(
          (guest) =>
            guest.expiration && new Date(guest.expiration) >= new Date(),
        );
        const inactiveGuests = response.filter(
          (guest) => new Date(guest.expiration) < new Date(),
        );
        setGuests({
          activeGuests: [...activeGuests, newGuest],
          inactiveGuests: inactiveGuests,
        });
      }
    } catch (error) {
      console.error("Error submitting guest:", error);
    }
  }

  return (
    <div className="min-h-screen p-4 md:p-6">
      <h1 className="mt-12 mb-4 font-heading font-medium text-3xl">
        {" "}
        Manage Guests
      </h1>
      <div className="flex flex-col items-center ">
        <div className="w-full space-y-10 md:flex  md:space-x-10">
          <div className="flex-none w-full md:w-2/3 md:mt-10">
            <GuestForm onSubmit={handleSubmit} editGuest={editGuest} />
          </div>
          <div className="md:flex flex-col flex-grow space-y-8">
            <CurrentGuestTable activeGuests={guests?.activeGuests || []} />
            <PreviousGuests
              inactiveGuests={guests?.inactiveGuests || []}
              setEditGuest={setEditGuest}
            />
          </div>
        </div>
      </div>
    </div>
  );
}
