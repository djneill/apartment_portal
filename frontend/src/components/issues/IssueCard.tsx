import React from "react";
import { ArrowUpRight } from "lucide-react";

interface IssueCardProps {
  date: string;
  title: string;
  isNew?: boolean;
  disabled?: boolean;
  type: string;
  onClick?: () => void;
}

const IssueCard: React.FC<IssueCardProps> = ({
  date,
  title,
  isNew = false,
  type = "",
  disabled = false,
  onClick,
}) => {
  return (
    <article className="relative p-4 bg-white rounded-3xl shadow-[0_4px_8px_rgba(0,0,0,0.15)] border border-black  w-72">
      {/* Flex container for date, New Issue badge, and arrow button */}
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-2">
          <div className="p-3 w-20 text-xs text-center text-white bg-black rounded-3xl">
            {date}
          </div>

          {isNew && (
            <div className="p-3 text-xs text-white bg-orange-400 rounded-[100px] w-[90px] text-center">
              New Issue
            </div>
          )}
        </div>

        <button
          className={`flex justify-center items-center w-10 h-10 bg-white border border-black border-solid rounded-[100px] ${
            disabled ? "opacity-50 cursor-not-allowed" : "cursor-pointer"
          }`}
          onClick={onClick}
          disabled={disabled}
        >
          <ArrowUpRight />
        </button>
      </div>

      <div className="mt-4 text-sm font-semibold">{type}</div>

      <h3 className="mt-1 text-2xl truncate" title={title}>
        {title}
      </h3>
    </article>
  );
};

export default IssueCard;
