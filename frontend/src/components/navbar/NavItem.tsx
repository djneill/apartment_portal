import * as React from "react";

import { Link } from "react-router-dom";

interface NavItemProps {
  icon: React.ReactNode;
  label: string;
  to: string;
  count?: number;
}

const NavItem: React.FC<NavItemProps & { isActive?: boolean }> = ({
  icon,
  label,
  to,
  count,
  isActive = false,
}) => {
  return (
    <Link to={to}>
      <div
        className={`flex justify-between items-center px-6 py-3 mt-3 w-full text-xl ${
          isActive ? "text-black bg-secondary" : "text-white"
        } rounded-3xl`}
      >
        <div className="flex gap-4 items-center whitespace-nowrap">
          {icon}
          <span>{label}</span>
        </div>
        {count !== undefined && (
          <div className="px-3 py-1 text-base text-black rounded-xl ">
            {count}
          </div>
        )}
      </div>
    </Link>
  );
};

export default NavItem;
