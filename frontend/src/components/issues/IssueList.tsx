import React, { useState, useEffect } from "react";
import IssueCard from "./IssueCard";
import issuesData from "../../data/issues.json";

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
    try {
      setIssues(issuesData);
    } catch (err) {
      setError("Failed to load issues");
    } finally {
      setLoading(false);
    }
  }, []);

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

      {/* Carousel Container */}
      <div className="overflow-x-auto whitespace-nowrap scroll-smooth bg-background">
        <div className="inline-flex gap-5">
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
      </div>
    </section>
  );
};

export default IssuesList;