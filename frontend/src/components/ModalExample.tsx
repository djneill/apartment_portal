import { useState } from 'react';
import Modal from './Modal';
import MainButton from './MainButton';

const ModalExample = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const openModal = () => setIsModalOpen(true);
  const closeModal = () => setIsModalOpen(false);

  return (
    <div className="p-6">
      <MainButton onClick={openModal}>Open Modal</MainButton>

      <Modal
        isOpen={isModalOpen}
        onClose={closeModal}
        title="Example Modal"
      >
        <div className="space-y-4">
          <p className="text-gray-700">
            This is an example modal dialog window.
          </p>
          <div className="flex justify-end">
            <MainButton onClick={closeModal}>Close</MainButton>
          </div>
        </div>
      </Modal>
    </div>
  );
};

export default ModalExample;
