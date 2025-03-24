import { useState, useEffect, useCallback } from "react";
import { ArrowUpRight, PackagePlus } from "lucide-react";
import confetti from "canvas-confetti";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import { getData, postData } from "../../services/api";
import useGlobalContext from "../../hooks/useGlobalContext";

interface PackageData {
  id: number;
  lockerNumber: number;
  code: number;
  status: {
    id: number;
    name: string;
  };
  unit: {
    id: number;
    number: string;
    price: number;
    statusId: number;
  };
}

interface PackageCardProps {
  packageCount?: number;
  userId?: number;
  unitNumber?: string;
  unitId?: number;
}

const PackageCard = ({ packageCount = 0, userId, unitId, unitNumber }: PackageCardProps) => {
  const [clicks, setClicks] = useState(0);
  const [showSbPackage, setShowSbPackage] = useState(false);
  const [showViewCodeModal, setShowViewCodeModal] = useState(false);
  const [showAddPackageModal, setShowAddPackageModal] = useState(false);
  const [packageData, setPackageData] = useState<PackageData[] | null>(null);
  const [lockerNumber, setLockerNumber] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [codeRevealed, setCodeRevealed] = useState(false);
  const { user } = useGlobalContext();
  const finalUserId = userId ?? user?.userId;

  const hideSbPackage = useCallback(() => {
    setShowSbPackage(false);
    setClicks(0);
  }, []);

  const fetchPackageData = async () => {
    if (!finalUserId) {
      console.error("User ID not available");
      return;
    }

    setIsLoading(true);
    try {
      const data = await getData<PackageData[]>(
        `Package?userId=${finalUserId}`
      );
      setPackageData(data);
    } catch (error) {
      console.error("Error fetching package data:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    if (finalUserId && packageCount > 0) {
      fetchPackageData();
    }
  }, [finalUserId, packageCount]);

  const handleAddPackage = async () => {
    setIsSubmitting(true);
    try {

      const payload = {
        unitId: unitId,
        lockerNumber: Number(lockerNumber),
        statusId: 6,
      }
      console.log("Submitting:", payload);

      await postData("/Package", payload);
  
      setLockerNumber("");
      setShowAddPackageModal(false);
    } catch (error) {
      console.error("Error adding package:", error);
    } finally {
      setIsSubmitting(false);
    }
  };
  

  const handleOpenViewCodeModal = () => {
    if (!packageData) {
      fetchPackageData();
    }
    setShowViewCodeModal(true);
    setCodeRevealed(false);
  };

  const handleRevealCode = () => {
    setCodeRevealed(true);
  };

  const handleOpenAddPackageModal = () => {
    setShowAddPackageModal(true);
  };

  useEffect(() => {
    let timer: number;
    if (clicks === 3) {
      setShowSbPackage(true);

      confetti({
        particleCount: 200,
        spread: 70,
        origin: { y: 0.6 },
        colors: ["#004C54", "#A5ACAF"],
      });
      timer = window.setTimeout(hideSbPackage, 3000);
    }

    return () => {
      if (timer) {
        window.clearTimeout(timer);
      }
    };
  }, [clicks, hideSbPackage]);

  const arrivedPackage = packageData?.find(
    (pkg) => pkg.status.name === "Arrived"
  );

  const lockerDisplay = arrivedPackage ? arrivedPackage.lockerNumber : "OFC";

  return (
    <div>
      <h3 className="text-sm font-semibold text-dark-gray mb-4 font-heading">
        Packages
      </h3>
      <Card className="bg-white rounded-xl p-4">
        <div className="flex justify-between items-center mb-2 mr-2">
          <span className="text-md font-bold">Locker #{lockerDisplay}</span>
          
            {user?.roles?.includes("Admin") ? (
              <div
              className="bg-primary rounded-full p-[4px] cursor-pointer"
              onClick={handleOpenAddPackageModal}
            >
              <PackagePlus className="text-white" />
              </div>
            ) : (              
              <div
            className="bg-primary rounded-full p-[4px] cursor-pointer"
            onClick={handleOpenViewCodeModal}
          >
            <ArrowUpRight className="text-white" />
          </div>
            )}
        </div>
        <div className="text-black text-xs py-3">
          <span
            onClick={() => setClicks((prev) => prev + 1)}
            className="cursor-default select-none"
          >
            📦{" "}
          </span>
          <span>
            {packageCount} {packageCount === 1 ? "Package" : "Packages"}{" "}
            Available
          </span>
        </div>
      </Card>

      <Modal isOpen={showViewCodeModal} onClose={() => setShowViewCodeModal(false)} title="">
        {isLoading ? (
          <div className="p-4 text-center">
            <p>Loading package details...</p>
          </div>
        ) : packageData ? (
          <div className="flex flex-col items-center text-center">
            <h2 className="text-2xl font-heading font-semibold mb-6">
              Locker #{lockerDisplay}
            </h2>

            {codeRevealed ? (
              <div className="w-full mb-4">
                <div className="bg-primary text-white py-3 px-6 rounded-4xl text-center text-xl font-heading font-semibold">
                  {arrivedPackage?.code}
                </div>
              </div>
            ) : (
              <button
                onClick={handleRevealCode}
                className="w-full bg-primary cursor-pointer text-white py-3 px-6 rounded-4xl mb-4 text-xl font-heading font-semibold"
              >
                Reveal Code
              </button>
            )}
          </div>
        ) : (
          <div className="p-4 text-center">
            <p>No package information available</p>
          </div>
        )}
      </Modal>

      <Modal
  isOpen={showAddPackageModal}
  onClose={() => setShowAddPackageModal(false)}
  title="Add Package"
>
  <div>
    <label className="block text-sm font-medium mb-1">Locker Number</label>
    <input
      type="text"
      value={lockerNumber}
      onChange={(e) => setLockerNumber(e.target.value)}
      placeholder="e.g. #A12"
      className="w-full mb-4 border-b-1 outline-none text-sm py-2"
    />

    <label className="block text-sm font-medium mb-1">Unit Number</label>
    <input
      type="text"
      value={unitNumber ?? ""}
      readOnly
      placeholder="e.g. 204"
      className="w-full mb-4 border-b-1 outline-none text-sm py-2"
    />

    <label className="block text-sm font-medium mb-1">Status</label>
    <select
      className="w-full mb-4 border-b-2 outline-none text-sm py-2"
      defaultValue="Arrived"
      disabled
    >
    <option value="Arrived">Arrived</option>
    </select>

    <button
  onClick={handleAddPackage}
  className="w-full bg-neutral-800 text-white py-3 px-6 cursor-pointer rounded-full mt-2 text-sm font-semibold"
  disabled={isSubmitting || !lockerNumber}
>
  {isSubmitting ? "Adding..." : "Add Package"}
</button>
  </div>
</Modal>

      {showSbPackage && (
        <div
          className="fixed inset-0 flex items-center justify-center bg-black/50 z-50"
          onClick={hideSbPackage}
        >
          <img
            src="src/tenantDashboard/components/dashAssets/asset/__dash__.png"
            alt="You Found The Hidden Surprise"
            className="max-w-md w-full rounded-lg shadow-lg p-8 bg-white"
            onClick={(e) => e.stopPropagation()}
          />
        </div>
      )}
    </div>
  );
};

export default PackageCard;
