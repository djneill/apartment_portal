import React from "react";
import { ArrowUpRight } from "lucide-react";

interface IssueCardProps {
  date: string;
  title: string;
  isNew?: boolean;
  onClick?: () => void;
}

const IssueCard: React.FC<IssueCardProps> = ({
  date,
  title,
  isNew = false,
  onClick,
}) => {

  return (
    <article className={`relative p-4 bg-white rounded-3xl shadow-[0_4px_8px_rgba(0,0,0,0.15)] border border-black  w-72 ${!isNew ? "border-dashed" : ""}`}>
      {/* Flex container for date, New Issue badge, and arrow button */}
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-2 ">
          <div className="p-3  text-xs text-center text-white bg-black rounded-full w-24">
            {date}
          </div>


          <div className={`p-3 text-xs text-white ${isNew ? "bg-orange-400" : "bg-primary"} rounded-full w-24 text-center`}>
            {isNew ? "New Issue" : "Resolved"}
          </div>

        </div>

        <button
          className={`flex justify-center items-center w-10 h-10 bg-white border border-black rounded-[100px] ${!isNew ? "opacity-50 cursor-not-allowed border-dashed" : "cursor-pointer"
            } `}
          onClick={onClick}
          disabled={isNew}
        >
          <ArrowUpRight />
        </button>
      </div>

      <div className="mt-4 text-sm font-semibold">Issue Type</div>

      <h3 className="mt-1 text-2xl line-clamp-1">{title}</h3>
    </article>
  );
};

export default IssueCard;
