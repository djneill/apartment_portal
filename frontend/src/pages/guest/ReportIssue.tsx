import React from "react";
import IssuesList from "./IssueList";
import IssueForm from "./IssueForm";

const ReportIssuesPage: React.FC = () => {
  return (
    <>
      <link
        href="https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700&display=swap"
        rel="stylesheet"
      />
      <main className="px-5 pt-14 pb-16 min-h-screen bg-zinc-100">
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
