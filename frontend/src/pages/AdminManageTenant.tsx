import { ArrowLeft } from "lucide-react"
import { useNavigate, useParams } from "react-router-dom";
import GuestProfileIcon from "../components/guests/GuestProfileIcon";
import LeaseCountdown from "../components/LeaseCountdown";
import { PackageCard } from "../tenantDashboard/components";
import IssuesList from "../components/issues/IssueList";
import { Guest, Packages, User } from "../types";
import CurrentGuestTable from "../components/guests/CurrentGuestTable";
import { useEffect, useState } from "react";
import { getData } from "../services/api";
import axios from "axios";

export default function AdminManageTenant() {
  const { id } = useParams<Record<string, string | undefined>>();
   const [guests, setGuests] = useState<{
      activeGuests: Guest[]
    } | null>(null);
   const [tenant, setTenant] = useState<User | null>(null);
   const [packages, setPackages] = useState< Packages[] | null>(null);


  const navigate = useNavigate()

  const goBack = () => {
    navigate(-1);
  };

  useEffect(() => {
    const fetchGuests = async () => {
      try {
        const response = await getData<Guest[]>(
          `/Guest?userId=${id}&active=true`
        );

        const activeGuests = response;
        setGuests({ activeGuests });
      }catch (error: unknown) {
        if (axios.isAxiosError(error)) {
          if (error.response?.status === 404) {
            console.warn("No active guests found");
            setGuests({ activeGuests: [] });
          }
        } else {
          console.error("Error fetching guests:", error);
        }}
    };
    const fetchUser = async () => {
      try {
        const response = await getData<{ user: User; unit: User["unit"] }>(`/Users/${id}`);
        const userWithUnit: User = {
          ...response.user,
          unit: response.unit
        };
    
        setTenant(userWithUnit);
      } catch (error) {
        console.error("Error fetching user details:", error);
      }
    }
    const fetchPackages = async () => {
      try {
        const response = await getData<Packages[]>(`Package/?userId=${id}&statusId=6`);
        setPackages(response);
      } catch (error) {
        console.error("Error fetching package data:", error);
      } 
    }

    fetchUser();
    fetchPackages();
    fetchGuests();
    },[id]); 

    const packageCount = packages?.length || 0;


  return (
    <div className="px-4 mt-14 font-heading min-h-screen space-y-6">
      <button className="rounded-full w-12 h-12 bg-[#d9d9d9] flex items-center justify-center absolute top-12 left-5" onClick={goBack}>
        <ArrowLeft size={30} color="#000000" />
      </button>
      <h1 className="text-center text-2xl font-heading md:text-left md:text-3xl">Manage Tenant</h1>

      <div className="md:flex space-y-6 md:space-x-4">
        <div className="flex space-x-4 items-center md:w-1/2">
          <GuestProfileIcon size={100} iconSize={60} />
          <div className="flex flex-col space-y-1">
            <p className="text-2xl font-medium">{tenant?.firstName} {tenant?.lastName}</p>
            <p className="text-xl">Unit {tenant?.unit?.unitNumber}</p>
          </div>
        </div>

        <LeaseCountdown userId={Number(id)}/>


      </div>

      <PackageCard packageCount={packageCount} userId={Number(id)} />

      <IssuesList userId={Number(id)}/>


      <CurrentGuestTable 
      activeGuests={guests?.activeGuests || []}
       />

    </div>
  )
}
