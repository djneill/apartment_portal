import { useState } from "react";
import GuestProfileIcon from "./GuestProfileIcon";
import { Plus } from "lucide-react";
import { Guest } from "../../Types";

export const PreviousGuests = ({
  inactiveGuests,
  setEditGuest,
}: {
  inactiveGuests: Guest[];
  setEditGuest: (guest: Guest) => void;
}) => {
  const [isExpanded, setIsExpanded] = useState(false);

  function PlusOverlay() {
    return (
      <div className="absolute top-0 right-0 flex items-center justify-center bg-accent text-black rounded-full w-6 h-6">
        <Plus size={20} />
      </div>
    );
  }

  const displayPrevGuests = isExpanded
    ? inactiveGuests
    : inactiveGuests.slice(0, 8);

  const mapPreviousGuests = displayPrevGuests.map((guest, index) => {
    const name = `${guest.firstName} ${guest.lastName}`;
    return (
      <div key={index} className="flex flex-col items-center space-y-2 ">
        <div
          className="relative inline-block cursor-pointer"
          onClick={() => setEditGuest(guest)}
        >
          <GuestProfileIcon size={65} iconSize={40} />
          <PlusOverlay />
        </div>
        <p className="text-sm">
          {name
            .split(" ")
            .map((part, index, arr) =>
              index === arr.length - 1 ? part.charAt(0) + "." : part,
            )
            .join(" ")}
        </p>
      </div>
    );
  });

  return (
    <>
      <div className="flex justify-between mb-4">
        <h2 className="font-bold text-[#686868]">Previous Guests</h2>
        <p
          className="font-bold cursor-pointer"
          onClick={() => setIsExpanded((prev) => !prev)}
        >
          {isExpanded ? "View less" : "View all"}
        </p>
      </div>

      <div className="bg-white rounded-2xl min-h-40 p-3">
        <div className="grid grid-cols-4 grid-rows-2 gap-y-4">
          {mapPreviousGuests}
        </div>
      </div>
    </>
  );
};

export default PreviousGuests;
