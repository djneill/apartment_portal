import React, { ReactNode } from "react";
import { X } from "lucide-react";

interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
  title?: string;
}

const Modal: React.FC<ModalProps> = ({ isOpen, onClose, children, title }) => {
  if (!isOpen) return null;

  return (
    <div
      className="fixed inset-0 bg-black/30 bg-opacity-50 flex items-center justify-center z-50 "
      onClick={onClose}
    >
      <button
        className="absolute top-3 right-3 text-gray-500 hover:text-gray-700 bg-transparent border-none text-xl cursor-pointer"
        onClick={onClose}
        aria-label="Close"
      >
        <X color="white" />
      </button>

      <div
        className="bg-white rounded-xl p-5 max-w-md w-[90%] relative shadow-lg "
        onClick={(e) => e.stopPropagation()}
      >
        {title && <h2 className="text-xl font-semibold mb-4">{title}</h2>}
        <div className="mt-2 ">{children}</div>
      </div>
    </div>
  );
};

export default Modal;
