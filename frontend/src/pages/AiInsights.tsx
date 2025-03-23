import { useState, useEffect } from "react";
import { TriangleAlert, AlertCircle } from "lucide-react";
import Card from "../components/Card";
import Modal from "../components/Modal";
import { FormTextArea } from "../components/form";
import { getData } from "../services/api";

interface Status {
  id: number;
  name: string;
}

interface Insight {
  id: number;
  title: string;
  summary: string;
  suggestion: string;
  createdOn: string;
  status: Status;
  actionTaken?: string;
}

interface InsightsData {
  currentInsights: Insight[];
  pastInsights: Insight[];
}

const InsightsPage = () => {
  const [insights, setInsights] = useState<InsightsData>({
    currentInsights: [],
    pastInsights: [],
  });
  const [selectedInsight, setSelectedInsight] = useState<Insight | null>(null);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [actionTaken, setActionTaken] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [showAllPastInsights, setShowAllPastInsights] =
    useState<boolean>(false);

  useEffect(() => {
    const fetchInsights = async () => {
      setIsLoading(true);
      try {
        const insightsData = await getData<InsightsData>("Insights");
        setInsights(insightsData);
      } catch (error) {
        console.error("Error fetching insights:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchInsights();
  }, []);

  const handleOpenModal = (insight: Insight) => {
    setSelectedInsight(insight);
    setActionTaken(insight.actionTaken || "");
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setSelectedInsight(null);
  };

  const toggleShowAllPastInsights = () => {
    setShowAllPastInsights(!showAllPastInsights);
  };

  const handleSaveAction = async () => {
    if (!selectedInsight) return;

    try {
      const updatedInsight = {
        ...selectedInsight,
        status: {
          id: 2,
          name: "Resolved",
        },
        actionTaken,
      };

      const updatedInsights = {
        currentInsights: insights.currentInsights.map((insight) =>
          insight.id === selectedInsight.id ? updatedInsight : insight
        ),
        pastInsights: insights.pastInsights.map((insight) =>
          insight.id === selectedInsight.id ? updatedInsight : insight
        ),
      };

      setInsights(updatedInsights);
      handleCloseModal();
    } catch (error) {
      console.error("Error updating insight:", error);
    }
  };

  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`;
  };

  const visiblePastInsights = showAllPastInsights
    ? insights.pastInsights
    : insights.pastInsights.slice(0, 3);

  if (isLoading) {
    return <div className="p-4">Loading insights...</div>;
  }

  return (
    <div className="p-4 mt-12">
      <div className="flex items-center mb-6">
        <h1 className="text-2xl font-heading">Insights</h1>
      </div>

      <h2 className="text-xl font-heading mb-4">Current Suggestions</h2>
      {insights.currentInsights.length > 0 ? (
        insights.currentInsights.map((insight) => (
          <div
            key={insight.id}
            className="mb-4"
            onClick={() => handleOpenModal(insight)}
          >
            <Card className="cursor-pointer">
              <div className="flex items-start gap-4">
                <div className="text-orange-500 mt-1">
                  <TriangleAlert size={24} />
                </div>
                <div className="flex-1">
                  <h3 className="font-semibold">{insight.title}</h3>
                  <p className="text-gray-700 line-clamp-2 overflow-hidden text-ellipsis">
                    {insight.summary}
                  </p>
                  <div className="mt-4">
                    <button
                      className="bg-gray-800 text-white px-4 py-2 rounded-full text-sm"
                      onClick={(e) => {
                        e.stopPropagation();
                        handleOpenModal(insight);
                      }}
                    >
                      Schedule Inspection
                    </button>
                  </div>
                </div>
              </div>
            </Card>
          </div>
        ))
      ) : (
        <p className="text-gray-500">No current suggestions available.</p>
      )}

      <div className="flex justify-between items-center mb-4 mt-8">
        <h2 className="text-normal font-heading text-dark-gray">
          Recent Suggestions
        </h2>
        <button
          className="text-accent text-sm"
          onClick={toggleShowAllPastInsights}
        >
          {showAllPastInsights ? "Show less" : "View all"}
        </button>
      </div>

      {insights.pastInsights.length > 0 ? (
        visiblePastInsights.map((insight) => (
          <div
            key={insight.id}
            className="mb-4"
            onClick={() => handleOpenModal(insight)}
          >
            <Card className="cursor-pointer">
              <div className="flex items-start gap-4">
                <div className="bg-gray-200 p-2 rounded-full">
                  <AlertCircle size={20} className="text-gray-600" />
                </div>
                <div className="flex-1">
                  <h3 className="font-semibold">{insight.title}</h3>
                  <p className="text-gray-700 line-clamp-2 overflow-hidden text-ellipsis">
                    {insight.summary}
                  </p>
                </div>
              </div>
            </Card>
          </div>
        ))
      ) : (
        <p className="text-gray-500">No recent suggestions available.</p>
      )}

      {selectedInsight && (
        <Modal isOpen={isModalOpen} onClose={handleCloseModal}>
          <h2 className="text-xl font-semibold mb-4">
            {selectedInsight.title}
          </h2>

          <div className="flex justify-between items-center mb-4">
            <div
              className={`px-4 py-1 rounded-full ${
                selectedInsight.status.name === "Resolved"
                  ? "bg-green-100 text-green-800"
                  : "bg-blue-100 text-blue-800"
              }`}
            >
              {selectedInsight.status.name}
            </div>
            <div>{formatDate(selectedInsight.createdOn)}</div>
          </div>

          <div className="mb-6">
            <h3 className="font-semibold mb-2">AI Suggestion:</h3>
            <p>{selectedInsight.suggestion}</p>
          </div>

          {selectedInsight.status.name === "Resolved" ? (
            <div>
              <h3 className="font-semibold mb-2">Action Taken:</h3>
              <p>{selectedInsight.actionTaken}</p>
            </div>
          ) : (
            <div>
              <FormTextArea
                label="What Action was taken?"
                value={actionTaken}
                onChange={(e) => setActionTaken(e.target.value)}
                placeholder="Describe the action taken..."
                rows={4}
              />
              <div className="flex justify-end mt-4">
                <button
                  className="bg-gray-200 text-gray-800 px-8 py-2 rounded-full"
                  onClick={handleSaveAction}
                >
                  Save
                </button>
              </div>
            </div>
          )}
        </Modal>
      )}
    </div>
  );
};

export default InsightsPage;
