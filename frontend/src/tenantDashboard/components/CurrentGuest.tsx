import { Plus } from 'lucide-react';
import ProfileImage from './ProfileImage';

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
            <div className="flex justify-between items-center mb-4">
                <h2 className="text-lg font-medium text-black">Current Guests</h2>
                <button
                    onClick={onViewAll}
                    className="text-sm text-black cursor-pointer hover:text-secondary/80 transition-colors"
                >
                    View all
                </button>
            </div>
            <div className="flex gap-4">
                <div className="flex flex-col items-center gap-2">
                    <button
                        onClick={onAddGuest}
                        className="w-12 h-12 bg-accent cursor-pointer rounded-full flex items-center justify-center hover:bg-accent/90 transition-colors"
                    >
                        <Plus className="text-white" size={20} />
                    </button>
                    <span className="text-xs">Add New</span>
                </div>
                {guests.map((guest, index) => (
                    <div key={index} className="flex flex-col items-center gap-2">
                        <ProfileImage alt={guest} className="bg-background" />
                        <span className="text-xs">{guest}</span>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default CurrentGuests;