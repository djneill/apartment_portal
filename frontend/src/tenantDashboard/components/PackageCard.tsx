import { useState, useEffect, useCallback } from "react";
import { ArrowUpRight } from "lucide-react";
import confetti from "canvas-confetti";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import { getData } from "../../services/api";
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
}

const PackageCard = ({ packageCount = 0, userId }: PackageCardProps) => {
  const [clicks, setClicks] = useState(0);
  const [showSbPackage, setShowSbPackage] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [packageData, setPackageData] = useState<PackageData[] | null>(null);
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

  const handleOpenModal = () => {
    if (!packageData) {
      fetchPackageData();
    }
    setShowModal(true);
    setCodeRevealed(false);
  };

  const handleRevealCode = () => {
    setCodeRevealed(true);
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
          <div
            className="bg-primary rounded-full p-[4px] cursor-pointer"
            onClick={handleOpenModal}
          >
            <ArrowUpRight className="text-white" />
          </div>
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

      <Modal isOpen={showModal} onClose={() => setShowModal(false)} title="">
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
