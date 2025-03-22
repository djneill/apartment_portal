import { useState } from "react";
import GuestProfileIcon from "./GuestProfileIcon";
import { Guest } from "../../types";
import CountdownTimer from "../CountdownTimer";

export default function CurrentGuestTable({
  activeGuests,
}: {
  activeGuests: Guest[];
}) {
  const [isExpanded, setIsExpanded] = useState(false);

  console.log(activeGuests);
  const displayedGuests = isExpanded ? activeGuests : activeGuests.slice(0, 3);

  const mapGuests = displayedGuests.map((guest, index) => {
    const name = `${guest.firstName} ${guest.lastName}`;
    return (
      <tr key={index} className="p-10 ">
        <td className="p-2">
          <GuestProfileIcon iconSize={20} />
        </td>
        <td className="p-2 text-nowrap">
          {name.length > 15 ? name.substring(0, 15) + "..." : name}
        </td>
        <td>
          <CountdownTimer
            className="p-2 font-bold"
            expiration={guest.expiration}
          />
        </td>
      </tr>
    );
  });

  return (
    <div className="font-heading">
      <div className="flex justify-between mb-4">
        <h2 className="font-bold text-[#686868]">Current Guests</h2>
        <p
          className="font-bold cursor-pointer"
          onClick={() => setIsExpanded(!isExpanded)}
        >
          {isExpanded ? "View less" : "View all"}
        </p>
      </div>

      <div className="bg-white rounded-2xl min-h-40 p-3">
        <table className="w-full">
          <thead className="border-b border-gray-300">
            <tr className="py-2">
              <th className="p-2"></th>
              <th className="text-left p-2 font-medium">NAME</th>
              <th className="text-left p-2 font-medium text-sm">
                TIME REMAINING
              </th>
            </tr>
          </thead>
          <tbody>
            {activeGuests.length === 0 ? (
              <tr>
                <td colSpan={3} className="text-center py-4">
                  No guests found
                </td>
              </tr>
            ) : (
              mapGuests
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}
