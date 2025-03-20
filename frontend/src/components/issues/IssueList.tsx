import React, { useState, useEffect } from "react";
import IssueCard from "./IssueCard";
import useGlobalContext from "../../hooks/useGlobalContext";
import { getData } from "../../services/api";
import { ApiIssue } from "../../Types";

interface Issue {
  id: number;
  date: string;
  title: string;
  isNew: boolean;
  disabled: boolean;
  type: string;
}

const IssuesList: React.FC = () => {
  const [issues, setIssues] = useState<Issue[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const { user: globalUser } = useGlobalContext();
  console.log(globalUser);
  useEffect(() => {
    const fetchIssues = async () => {
      if (!globalUser?.userId) return;

      try {
        const data = await getData<ApiIssue[]>(`Issues?userId=${globalUser.userId}`);
        const mappedIssues = data.map((issue: any) => ({
          id: issue.id,
          date: new Date(issue.createdOn).toLocaleDateString(),
          title: issue.description,
          type: issue.issueType.name,
          isNew: true,
          disabled: issue.status.id !== 1, 
        }));
        setIssues(mappedIssues);
      } catch (err) {
        setError("Failed to load issues");
      } finally {
        setLoading(false);
      }
    };

    fetchIssues();
  }, [globalUser]);

  const handleViewAll = () => {
    console.log("Navigate to all issues page");
  };

  const handleIssueClick = (issueId: number) => {
    console.log(`Clicked on issue ${issueId}`);
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  return (
    <section className="">
      <div className="flex justify-between items-center mb-5 font-heading">
        <h2 className="text-sm font-bold text-stone-500">Latest Issues</h2>
        <button
          className="text-sm font-bold text-neutral-700 cursor-pointer"
          onClick={handleViewAll}
        >
          View all
        </button>
      </div>

      {/* Flex Container for Desktop */}
      <div className="hidden md:flex md:flex-wrap md:gap-5">
        {issues.map((issue) => (
          <IssueCard
            key={issue.id}
            date={issue.date}
            title={issue.title}
            type={issue.type}
            isNew={issue.isNew}
            disabled={issue.disabled}
            onClick={() => !issue.disabled && handleIssueClick(issue.id)}
          />
        ))}
      </div>

      <div className="md:hidden overflow-x-auto whitespace-nowrap scroll-smooth bg-background">
        <div className="inline-flex gap-5">
          {issues.map((issue) => (
            <IssueCard
              key={issue.id}
              date={issue.date}
              title={issue.title}
              type={issue.type}
              isNew={issue.isNew}
              disabled={issue.disabled}
              onClick={() => !issue.disabled && handleIssueClick(issue.id)}
            />
          ))}
        </div>
      </div>
    </section>
  );
};

export default IssuesList;