import React, { useState } from "react";
import useGlobalContext from "../hooks/useGlobalContext";
import LeasePreview from "../components/LeasePreview";
import { ExternalLinkIcon } from "lucide-react";
import MainButton from "../components/MainButton";
import Modal from "../components/Modal";
import LeaseSignature from "../components/LeaseSignature";

const ManageLease: React.FC = () => {
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const { user } = useGlobalContext();
  const fullName = user?.firstName + " " + user?.lastName;

  const pdfUrl = "../../public/LEASE RENEWAL AGREEMENT.pdf";
  const imageUrl = "../../public/lease-snapshot.jpg";

  return (
    <div className="w-full min-h-screen p-5 md:p-6">
      <h1 className="mt-20  mb-5 font-heading font-medium text-3xl">
        {" "}
        Manage Lease
      </h1>

      <div className="bg-white p-5 h-96 rounded-2xl flex flex-col items-center justify-center space-y-4">
        <LeasePreview
          previewImageUrl={imageUrl}
          pdfUrl={pdfUrl}
          containerHeight="300px"
        />
        <div className="flex self-start">
          <a
            href={pdfUrl}
            target="_blank"
            rel="noopener noreferrer"
            className="font-semibold flex"
          >
            <p className="mr-2">View Lease</p>
            <ExternalLinkIcon />
          </a>
        </div>
      </div>

      <MainButton
        onClick={() => setIsModalOpen((prev) => !prev)}
        className="w-full md:w-72 h-14 rounded-2xl mt-5"
      >
        Sign Lease
      </MainButton>

      <Modal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        title="Verify Lease Signature"
      >
        <div className="space-y-5">
          <p className="">
            By signing, you acknowledge that you are entering into a legally
            binding lease agreement. Please review all the terms carefully
            before proceeding. Your signature confirms that you have read,
            understood, and agreed to be bound by every provision of the lease.
            If you have any questions or need clarification, consult your legal
            advisor prior to signing.
          </p>

          <LeaseSignature fullName={fullName} />

          <MainButton
            onClick={() => setIsModalOpen((prev) => !prev)}
            className="w-full h-14 rounded-2xl mt-10"
          >
            Sign Lease
          </MainButton>
        </div>
      </Modal>
    </div>
  );
};

export default ManageLease;
