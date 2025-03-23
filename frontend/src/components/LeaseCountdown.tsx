import { useEffect, useState } from "react";
import { getData } from "../services/api";
import { Lease } from "../Types";
import useGlobalContext from "../hooks/useGlobalContext";

interface LeaseCountdownProps {
  userId?: number;
}

export default function LeaseCountdown({ userId }: LeaseCountdownProps) {
  const [endDate, setEndDate] = useState<Date | null>(null);
  const [timeLeft, setTimeLeft] = useState({
    days: 0,
    hours: 0,
    minutes: 0,
    seconds: 0,
  });
  const { user } = useGlobalContext();
  const finalUserId = userId ?? user?.userId;

  useEffect(() => {
    if (!finalUserId) {
      console.error("User ID not available");
      return;
    }

    const fetchData = async () => {
      try {
        const response = await getData<Lease[]>(
          `LeaseAgreements?userId=${finalUserId}`
        );
        const activeLease = response.find(
          (lease) => lease.status?.name === "Active"
        );

        if (activeLease?.endDate) {
          const parsedEnd = new Date(activeLease.endDate);
          setEndDate(parsedEnd);
        } else {
          console.warn("No active lease found");
        }
      } catch (error) {
        console.error("Error fetching lease data:", error);
      }
    };

    fetchData();
  }, [finalUserId]);

  useEffect(() => {
    if (!endDate) return;

    const calculateTimeLeft = () => {
      const now = new Date().getTime();
      const distance = endDate.getTime() - now;

      if (distance <= 0) {
        setTimeLeft({ days: 0, hours: 0, minutes: 0, seconds: 0 });
        return;
      }

      const days = Math.floor(distance / (1000 * 60 * 60 * 24));
      const hours = Math.floor((distance / (1000 * 60 * 60)) % 24);
      const minutes = Math.floor((distance / (1000 * 60)) % 60);
      const seconds = Math.floor((distance / 1000) % 60);

      setTimeLeft({ days, hours, minutes, seconds });
    };

    calculateTimeLeft();
    const interval = setInterval(calculateTimeLeft, 1000);

    return () => clearInterval(interval);
  }, [endDate]);

  return (
    <div className="w-full bg-black font-heading rounded-2xl p-5 md:w-1/2">
      <div className="flex items-center justify-between mb-4">
        <h1 className="text-white">Lease Countdown</h1>
        <button className="bg-white text-black rounded-full px-4 py-2 font-semibold text-sm cursor-pointer">
          Send Notification
        </button>
      </div>
      <div className="flex text-white items-center space-x-5">
        <div className="flex flex-col items-center">
          <p className="text-xl">{timeLeft.days}</p>
          <p className="text-[#919397] text-sm">Days</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div>

        <div className="flex flex-col items-center">
          <p className="text-xl">{timeLeft.hours}</p>
          <p className="text-[#919397] text-sm">Hours</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div>
        <div className="flex flex-col items-center">
          <p className="text-xl">{timeLeft.minutes}</p>
          <p className="text-[#919397] text-sm">Minutes</p>
        </div>
        <div className="border-l-1 h-5 border-white "></div>
        <div className="flex flex-col items-center">
          <p className="text-xl">{timeLeft.seconds}</p>
          <p className="text-[#919397] text-sm">Seconds</p>
        </div>
      </div>
    </div>
  );
}
