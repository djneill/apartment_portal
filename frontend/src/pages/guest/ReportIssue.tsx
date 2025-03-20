import React from "react";
import IssuesList from "../../components/issues/IssueList";
import IssueForm from "../../components/issues/IssueForm";

const ReportIssuesPage: React.FC = () => {
  return (
    <>
      <main className="px-5 pt-14 pb-16 min-h-screen">
        <header className="mb-10">
          <h1 className="text-3xl font-medium text-black">Report Issues</h1>
        </header>

        <div className="flex flex-col md:flex-row gap-5">
          <div className="md:w-1/2">
            <IssueForm />
          </div>
          <div className="md:w-1/2">
            <IssuesList />
          </div>
        </div>
      </main>
    </>
  );
};

export default ReportIssuesPage;