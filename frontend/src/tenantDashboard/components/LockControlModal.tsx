import { useState } from "react";
import { Lock, Unlock } from "lucide-react";
import Modal from "../../components/Modal";

interface LockControlModalProps {
  isOpen: boolean;
  onClose: () => void;
}

const LockControlModal = ({ isOpen, onClose }: LockControlModalProps) => {
  const [isLocked, setIsLocked] = useState(false);

  const toggleLock = () => {
    setIsLocked(!isLocked);
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <div className="flex flex-col items-center gap-6">
        <h2 className="text-2xl font-heading font-bold text-center">
          Door Control
        </h2>
        <div className="flex flex-col items-center gap-4">
          <button
            onClick={toggleLock}
            className={`w-24 h-24 rounded-full flex items-center justify-center transition-colors cursor-pointer ${
              isLocked
                ? "bg-red-500 text-white hover:bg-red-600"
                : "bg-green-500 text-white hover:bg-green-600"
            }`}
          >
            {isLocked ? <Lock size={32} /> : <Unlock size={32} />}
          </button>
          <p className="text-lg font-medium text-black">
            {isLocked ? "Door is Locked" : "Door is Unlocked"}
          </p>
        </div>
      </div>
    </Modal>
  );
};

export default LockControlModal;
