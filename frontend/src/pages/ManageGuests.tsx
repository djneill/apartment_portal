import { useEffect, useState } from "react";
import CurrentGuestTable from "../components/guests/CurrentGuestTable";
import GuestForm from "../components/guests/GuestForm";
import PreviousGuests from "../components/guests/PreviousGuests";
import { getData, postData } from "../services/api";
import { Guest, GuestsResponse, GuestRequest } from "../types";
import { AxiosError } from "axios";

export default function ManageGuests() {
  const [guests, setGuests] = useState<{ activeGuests: Guest[]; inactiveGuests: Guest[] } | null>(null);

  //TODO: change countdown to have days, hours, mins
  async function fetchActiveAndInactiveGuests() {
    try {
      const [activeGuestsResponse, inactiveGuestsResponse] = await Promise.all([
        getData<GuestsResponse>("Guest?userId=6&active=true"),
        getData<GuestsResponse>("Guest?userId=6&active=false"),
      ])

      const activeGuests: Guest[] = activeGuestsResponse.data || [];
      const inactiveGuests: Guest[] = inactiveGuestsResponse.data || [];

      if (activeGuestsResponse.message === "No guests found.") {
        console.log("No active guests found.");
      }
      if (inactiveGuestsResponse.message === "No guests found.") {
        console.log("No inactive guests found.");
      }


      return { activeGuests, inactiveGuests };

    } catch (error) {
      const axiosError = error as AxiosError;

      if (axiosError.response && axiosError.response.status === 404) {
        const responseData = axiosError.response.data as { message?: string };
        if (responseData.message === "No guests found.") {
          return { activeGuests: [], inactiveGuests: [] };
        }
      }

      console.error("Error fetching guests:", axiosError);
      throw axiosError;
    }
  }

  async function createGuest(guest: GuestRequest) {
    try {
      postData("Guest/register-guest", guest)
    } catch (error) {
      const axiosError = error as AxiosError
      console.error(axiosError)
    }
  }


  function handleSubmit(data: GuestRequest) {
    console.log(data)
    createGuest(data)
  }

  useEffect(() => {
    async function loadGuests() {
      try {
        const guestData = await fetchActiveAndInactiveGuests()
        setGuests(guestData)
        // console.log(guestData)
      } catch (error) {
        console.error("Error loading guests", error)
        throw error
      }
    }
    loadGuests()
  }, [])




  return (
    <div className="w-screen min-h-screen p-4">
      <h1 className="mt-12 mb-4 font-heading font-medium text-3xl">Manage Guests</h1>
      <div className="flex flex-col items-center ">
        <div className="w-full space-y-10 md:flex  md:space-x-10">
          <div className="flex-none w-full md:w-2/3 md:mt-10">
            <GuestForm onSubmit={handleSubmit} />
          </div>
          <div className="md:flex flex-col flex-grow space-y-8">
            <CurrentGuestTable activeGuests={guests?.activeGuests || []} />
            <PreviousGuests inactiveGuests={guests?.inactiveGuests || []} />
          </div>
        </div>
      </div>
    </div>
  )
}
