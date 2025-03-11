import React from 'react';
import { UserRound } from 'lucide-react';

interface GuestProfileIconProps {
  size?: number;
  iconSize?: number;
  bgColor?: string;
  iconColor?: string;
}

const GuestProfileIcon: React.FC<GuestProfileIconProps> = ({
  size = 35,
  iconSize = 25,
  bgColor = "#D9D9D9",
  iconColor = "black",
}) => {
  return (
    <div
      className="flex items-center justify-center rounded-full"
      style={{ width: size, height: size, backgroundColor: bgColor }}
    >
      <UserRound className="text-black" size={iconSize} color={iconColor} />
    </div>
  );
};

export default GuestProfileIcon;
