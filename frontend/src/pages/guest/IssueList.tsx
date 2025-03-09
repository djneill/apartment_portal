import React, { useEffect, useState } from "react";
import IssueCard from "./IssueCard";
import { getData } from "../../services/api"; 

interface Issue {
  id: number;
  date: string;
  title: string;
  isNew: boolean;
  disabled: boolean;
}

const IssuesList: React.FC = () => {
  const [issues, setIssues] = useState<Issue[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchIssues = async () => {
      try {
        const data = await getData<Issue[]>("/issues.json"); 
        setIssues(data);
      } catch (err) {
        setError("Failed to fetch issues");
      } finally {
        setLoading(false);
      }
    };

    fetchIssues();
  }, []);

  const handleViewAll = () => {
    console.log("navigate to all issues page");
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
    <section className="mt-10">
      <div className="flex justify-between items-center mb-5">
        <h2 className="text-sm font-bold text-stone-500">Latest Issues</h2>
        <button
          className="text-sm font-bold text-neutral-700"
          onClick={handleViewAll}
        >
          View all
        </button>
      </div>

      <div className="flex overflow-x-auto gap-5 max-sm:flex-col">
        {issues.map((issue) => (
          <IssueCard
            key={issue.id}
            date={issue.date}
            title={issue.title}
            isNew={issue.isNew}
            disabled={issue.disabled}
            onClick={() => !issue.disabled && handleIssueClick(issue.id)}
          />
        ))}
      </div>
    </section>
  );
};

export default IssuesList;