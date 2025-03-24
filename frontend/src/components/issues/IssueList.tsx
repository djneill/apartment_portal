import React, { useState, useEffect } from "react";
import IssueCard from "./IssueCard";
import useGlobalContext from "../../hooks/useGlobalContext";
import { getData } from "../../services/api";
import { ApiIssue, Issue } from "../../Types";
import Modal from "../../components/Modal";
import { User, Calendar, FileText } from "lucide-react"; 

interface IssuesListProps {
  userId?: number;
}

const IssuesList: React.FC<IssuesListProps> = ({ userId }) => {
  const [issues, setIssues] = useState<Issue[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const { user: globalUser } = useGlobalContext();
  const finalUserId = userId ?? globalUser?.userId;
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [selectedIssue, setSelectedIssue] = useState<Issue | null>(null);

  useEffect(() => {
    const fetchIssues = async () => {
      if (!finalUserId) return;

      try {
        const data = await getData<ApiIssue[]>(
          `Issues?userId=${finalUserId}&recordRetrievalCount=10&statusId=0&orderByDesc=true`
        );
        const mappedIssues = data.map((issue: ApiIssue) => {
          const currentDate = new Date();
          const issueDate = new Date(issue.createdOn);
          const timeDifference = currentDate.getTime() - issueDate.getTime();
          const daysDifference = timeDifference / (1000 * 3600 * 24);
          const isNew = daysDifference <= 3;
          const status = issue.status.name === "Active" ? "Created" : "Resolved";
          return {
            id: issue.id,
            date: issueDate.toLocaleDateString(),
            title: issue.description,
            type: issue.issueType.name,
            isNew: isNew,
            disabled: issue.status.id !== 1,
            status: status,
            created: issue.createdOn,
            user: `${issue.user.firstName} ${issue.user.lastName}`,
          };
        });
        setIssues(mappedIssues);
      } catch (err) {
        console.error(err);
        setError("Failed to load issues");
      } finally {
        setLoading(false);
      }
    };

    fetchIssues();
  }, [finalUserId]);

  const handleViewAll = () => {
    console.log("Navigate to all issues page");
  };

  const handleIssueClick = (issueId: number) => {
    const issue = issues.find((issue) => issue.id === issueId);
    if (issue) {
      setSelectedIssue(issue);
      setIsModalOpen(true);
    }
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setSelectedIssue(null);
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
            status={issue.status}
            onClick={() => !issue.disabled && handleIssueClick(issue.id)}
          />
        ))}
      </div>

      {/* Scrollable Container for Mobile */}
      <div className="md:hidden overflow-x-auto whitespace-nowrap scroll-smooth bg-background">
        <div className="inline-flex gap-5">
          {issues.map((issue) => (
            <IssueCard
              key={issue.id}
              date={issue.date}
              title={issue.title}
              type={issue.type}
              isNew={issue.isNew}
              status={issue.status}
              onClick={() => !issue.disabled && handleIssueClick(issue.id)}
            />
          ))}
        </div>
      </div>

      {selectedIssue && (
        <Modal isOpen={isModalOpen} onClose={handleCloseModal}>
          <h2 className="text-xl font-semibold mb-4">{selectedIssue.type}</h2>

          <div className="flex items-center mb-4">
            <div
              className={`p-3 mr-2 text-center text-xs rounded-full w-24 ${
                selectedIssue.status === "Resolved"
                  ? "bg-green-100 text-green-800"
                  : "bg-blue-100 text-blue-800"
              }`}
            >
              {selectedIssue.status}
            </div>
            {selectedIssue.isNew && (
              <div className="p-3 text-xs text-white bg-orange-300 rounded-full w-24 text-center">
                New Issue
              </div>
            )}
          </div>

          {/* Details Section with Icon */}
          <div className="mb-6">
            <div className="flex items-center gap-2 mb-2">
              <FileText className="w-5 h-5 text-gray-600" />
              <h3 className="font-semibold">Details:</h3>
            </div>
            <p className="pl-7">{selectedIssue.title}</p>
          </div>

          <div className="mb-6">
            <div className="flex items-center gap-2 mb-2">
              <Calendar className="w-5 h-5 text-gray-600" />
              <h3 className="font-semibold">Created on:</h3>
            </div>
            <p className="pl-7">{selectedIssue.date}</p>
          </div>

          <div className="mb-6">
            <div className="flex items-center gap-2 mb-2">
              <User className="w-5 h-5 text-gray-600" />
              <h3 className="font-semibold">Created by:</h3>
            </div>
            <p className="pl-7">{selectedIssue.user}</p>
          </div>

          <div className="flex justify-end mt-4">
            <button
              className="bg-gray-200 text-gray-800 px-8 py-2 rounded-full"
              onClick={handleCloseModal}
            >
              Close
            </button>
          </div>
        </Modal>
      )}
    </section>
  );
};

export default IssuesList;