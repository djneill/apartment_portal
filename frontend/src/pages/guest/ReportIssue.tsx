import React from "react";
import IssuesList from "../../components/issues/IssueList";
import IssueForm from "../../components/issues/IssueForm";

const ReportIssuesPage: React.FC = () => {
  return (
    <>
      <main className="px-5 pt-14 pb-16 min-h-screen ">
        <header className="mb-10">
          <h1 className="text-3xl font-medium text-black">Report Issues</h1>
        </header>

        <IssueForm />

        <IssuesList />
      </main>
    </>
  );
};

export default ReportIssuesPage;
