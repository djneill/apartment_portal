import React from "react";
import IssuesListAdmin from "../../components/issues/IssueListAdmin";

const AdminReportIssuesPage: React.FC = () => {
  return (
    <>
      <main className="px-5 pt-14 pb-16 min-h-screen">
        <header className="mb-10">
          <h1 className="text-3xl font-medium text-black">
            List of issues reports
          </h1>
        </header>

        <div className="flex flex-col md:flex-row gap-5">
          <IssuesListAdmin />
        </div>
      </main>
    </>
  );
};

export default AdminReportIssuesPage;
