import React, { useState } from "react";
import IssueCard from "./IssueCard";
import { Issue } from "../../pages/AdminDashboard";
import { useNavigate } from "react-router-dom";

interface IssuesListProps {
  issues: Issue[];
}

const IssuesList: React.FC<IssuesListProps> = ({ issues }) => {
  const [viewAllIssues, setViewAllIssues] = useState(false)
  const navigate = useNavigate()



  const handleIssueClick = (issueId: number) => {
    console.log(`Clicked on issue ${issueId}`);
  };

  const formatDate = (isoDate: string) => {
    const issueDate = new Date(isoDate)

    const month = (issueDate.getMonth() + 1).toString().padStart(2, "0"); // Months are 0-indexed
    const day = issueDate.getDate().toString().padStart(2, "0");
    const year = issueDate.getFullYear();

    return `${month}/${day}/${year}`;
  }

  const renderIssues = issues.map((issue) => {
    const formattedDate = formatDate(issue.createdOn)

    const isNew = issue.status.name === "Active" ? true : false

    return (
      <IssueCard
        key={issue.id}
        date={formattedDate}
        title={issue.description}
        isNew={isNew}
        onClick={() => !isNew && handleIssueClick(issue.id)}
      />
    )
  })

  return (
    <section className="">
      <div className="flex justify-between items-center mb-5 font-heading">
        <h2 className="text-sm font-bold text-stone-500">Latest Issues</h2>
        <button
          className="text-sm font-bold text-neutral-700 cursor-pointer"
          onClick={() => setViewAllIssues(prev => !prev)}
        >
          {viewAllIssues ? "View Less" : "View all"}
        </button>
      </div>

      <div className="-mr-10">
        <div className={`flex w-full overflow-scroll space-x-3 py-2 ${viewAllIssues ? "flex-col space-y-2 " : ""}`}>
          {renderIssues}
        </div>

      </div>
    </section >
  );
};

export default IssuesList;
