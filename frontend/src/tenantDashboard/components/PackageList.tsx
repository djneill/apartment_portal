import { useState } from "react";
import PackageCard from "./PackageCard";
import { Packages } from "../../Types";

interface PackageListProps {
  packages: Packages[];
  userId?: number;
  unitId?: number;
  unitNumber?: string;
  refetchPackages?: () => void;
}

const PackageList = ({ packages, userId, unitId, unitNumber, refetchPackages }: PackageListProps) => {
  const [expanded, setExpanded] = useState(false);

  const groupedPackages = packages.reduce<Record<number, Packages[]>>((acc, pkg) => {
    const locker = pkg.lockerNumber;
    if (!acc[locker]) acc[locker] = [];
    acc[locker].push(pkg);
    return acc;
  }, {});

  const lockerEntries = Object.entries(groupedPackages);

  const entriesToShow = expanded ? lockerEntries : lockerEntries.slice(0, 1);

  const hasPackages = lockerEntries.length > 0;

  return (
    <div className="space-y-4">
      <div className="flex justify-between items-center mb-4 font-heading text-sm font-semibold">
        <h2 className="text-dark-gray">Packages</h2>
        {lockerEntries.length > 0 && (
          <button
            onClick={() => setExpanded(!expanded)}
            className="text-primary cursor-pointer hover:text-secondary/80 transition-colors"
          >
            {expanded ? "Hide all" : "View all"}
          </button>
        )}
      </div>
      {hasPackages ? (
        entriesToShow.map(([locker, pkgs]) => (
          <PackageCard
            key={locker}
            packageCount={pkgs.length}
            userId={userId}
            unitNumber={unitNumber}
            unitId={unitId}
            lockerNumberProp={Number(locker)}
            packages={expanded ? pkgs : undefined}
            refetchPackages={refetchPackages}
          />
        ))
      ) : (
        <PackageCard
          packageCount={0}
          userId={userId}
          unitNumber={unitNumber}
          unitId={unitId}
          lockerNumberProp={undefined} 
          packages={[]}
        />
      )}
    </div>
  );
};

export default PackageList;
