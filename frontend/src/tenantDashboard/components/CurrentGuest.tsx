import { Plus } from "lucide-react";
import GuestProfileIcon from "../../components/guests/GuestProfileIcon";

interface CurrentGuestsProps {
  guests: string[];
  onAddGuest?: () => void;
  onViewAll?: () => void;
}

const CurrentGuests = ({
  guests,
  onAddGuest,
  onViewAll,
}: CurrentGuestsProps) => {
  return (
    <div>
      <div className="flex justify-between items-center mb-4 font-heading text-sm font-semibold">
        <h2 className=" text-dark-gray">Current Guests</h2>
        <button
          onClick={onViewAll}
          className="text-primary cursor-pointer hover:text-secondary/80 transition-colors"
        >
          View all
        </button>
      </div>
      <div className="flex justify-between ">
        <div className="flex flex-col items-center gap-2">
          <button
            onClick={onAddGuest}
            className="w-20 h-20 bg-accent cursor-pointer rounded-full flex items-center justify-center hover:bg-accent/90 transition-colors"
          >
            <Plus className="text-white" size={48} />
          </button>
          <span className="text-xs">Add New</span>
        </div>
        {guests.map((guest, index) => (
          <div key={index} className="flex flex-col items-center gap-2 ">
            <GuestProfileIcon size={80} iconSize={40} />
            <span className="text-xs">{guest}</span>
          </div>
        ))}
      </div>
    </div>
  );
};

export default CurrentGuests;
