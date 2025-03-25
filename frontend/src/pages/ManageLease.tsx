import React, { useCallback, useEffect, useState } from "react";
import useGlobalContext from "../hooks/useGlobalContext";
import LeasePreview from "../components/LeasePreview";
import { ExternalLinkIcon } from "lucide-react";
import MainButton from "../components/MainButton";
import Modal from "../components/Modal";
import LeaseSignature from "../components/LeaseSignature";
import { getData, patchData } from "../services/api";
import { useToast } from "../components/ToastProvider";

interface LeaseStatus {
  id: number;
  name: string;
}

interface UnitUser {
  userId: number;
  unitId: number;
}

interface Lease {
  id: number;
  startDate: string;
  endDate: string;
  link: string;
  signedOn: string | null;
  status: LeaseStatus;
  unitUser: UnitUser;
}

const ManageLease: React.FC = () => {
  const [leaseAgreement, setLeaseAgreement] = useState<Lease>();
  const [isSigned, setIsSigned] = useState<boolean>(false);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const { user } = useGlobalContext();
  const fullName = user?.firstName + " " + user?.lastName;

  const pdfUrl = "/LEASE RENEWAL AGREEMENT.pdf";
  const imageUrl = "/lease-snapshot.jpg";

  const { addToast } = useToast()

  const fetchLeaseAgreements = useCallback(async () => {
    if (!user?.userId) {
      console.error("User not logged in");
      return;
    }
    try {
      const response: Lease[] = await getData(
        `LeaseAgreements?userId=${user.userId}`,
      );
      if (response.length) {
        setLeaseAgreement(response[response.length - 1]);
      }
    } catch (error) {
      console.error(error);
    }
  }, [user]);

  const updateLeaseAgreement = async (leaseAgreement: Lease) => {
    const todayDate = new Date().toISOString().slice(0, 10);

    const newEnd = new Date();
    newEnd.setFullYear(newEnd.getFullYear() + 1);
    const newEndDate = newEnd.toISOString().slice(0, 10);

    const payload = {
      id: leaseAgreement.id,
      signedOn: todayDate,
      startDate: todayDate,
      endDate: newEndDate,
      statusId: 1,
    };

    try {
      await patchData(`LeaseAgreements/${leaseAgreement.id}`, payload);
      console.log("Lease agreement updated with payload:", payload);

      addToast("Success signing lease", {
        type: "success",
        duration: 3000,
      });


    } catch (error) {
      addToast("Error signing lease", {
        type: "error",
        duration: 3000,
      });

      console.error(error);
    }
  };

  const handleConfirmation = () => {
    if (leaseAgreement && isSigned) {
      setIsModalOpen(false);
      updateLeaseAgreement(leaseAgreement);
    }
  };

  useEffect(() => {
    fetchLeaseAgreements();
  }, [fetchLeaseAgreements]);

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

          <LeaseSignature
            fullName={fullName}
            isSigned={isSigned}
            setIsSigned={setIsSigned}
          />

          <MainButton
            onClick={handleConfirmation}
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
