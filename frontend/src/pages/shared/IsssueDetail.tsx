import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { getData } from "../../services/api";
import { ApiIssue } from "../../types";
import { AlertCircle, Bug, Calendar, User } from "lucide-react";

const IssueDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [issue, setIssue] = useState<ApiIssue | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  useEffect(() => {
    const fetchIssueDetail = async () => {
      try {
        const data = await getData<ApiIssue[]>(`Issues`);
        const filteredIssue = data.find(issue => issue.id === Number(id));
        if (!filteredIssue) {
          setError("Issue not found");
          return;
        }
        setIssue(filteredIssue);
      } catch (err) {
        console.error(err);
        setError("Error loading issue details");
      } finally {
        setLoading(false);
      }
    };

    fetchIssueDetail();
  }, [id]);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;
  if (!issue) return <div>Issue not found</div>;

  return (
    <main className="px-5 pt-14 pb-16 min-h-screen">
      <header className="mb-10">
        <h1 className="text-3xl font-medium text-black">Issue Detail</h1>
      </header>
    <div className="w-full items-center p-5 text-sm font-medium bg-white rounded-2xl">
    <div className="flex flex-col gap-5">
        <div className="flex items-center space-x-2">
          <span className="text-gray-500 text-sm">Issue #{issue.id}</span>
          <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
            issue.status.name === 'Open' ? 'bg-red-100 text-red-800' : 'bg-green-100 text-green-800'
          }`}>
            {issue.status.name}
          </span>
        </div>

        <div className="flex items-center space-x-2 text-sm text-gray-500">
          <Calendar className="w-4 h-4" />
          <span>{formatDate(issue.createdOn)}</span>
        </div>

        <div className="flex items-start space-x-3">
          <div className="flex-shrink-0">
            <AlertCircle className="w-6 h-6 text-red-500" />
          </div>
          <div>
            <p className="text-gray-900 text-lg font-medium">
              {issue.description}
            </p>
          </div>
        </div>

        <div className="flex flex-col gap-2">
          <div className="flex items-center space-x-2">
            <Bug className="w-5 h-5 text-gray-400" />
            <span className="text-sm text-gray-600">Type:</span>
            <span className="text-sm font-medium text-gray-900">{issue.issueType.name}</span>
          </div>
          <div className="flex items-center space-x-2">
            <User className="w-5 h-5 text-gray-400" />
            <span className="text-sm text-gray-600">Reported by:</span>
            <span className="text-sm font-medium text-gray-900">
              {issue.user.firstName} {issue.user.lastName}
            </span>
          </div>
        </div>
      </div>
    </div>
      
    </main>
  );
};

export default IssueDetail;