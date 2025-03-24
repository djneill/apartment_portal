import { useEffect, useState } from "react";
import { getData } from "../services/api";
import { useNavigate } from "react-router-dom";
import MainButton from "../components/MainButton";
import { Search } from "lucide-react";

interface LeaseAgreement {
  id: number;
  startDate: string;
  endDate: string;
  signedOn: string | null;
  link: string;
  unitUser: {
    userId: number;
    unitId: number;
  };
  status: {
    id: number;
    name: string;
  };
}

export const AdminManageLease = () => {
  const navigate = useNavigate();
  const [leaseAgreements, setLeaseAgreements] = useState<LeaseAgreement[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [currentPage, setCurrentPage] = useState(1);
  const agreementsPerPage = 10;
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchLeaseAgreements = async () => {
      try {
        const data = await getData<LeaseAgreement[]>("leaseAgreements");
        const today = new Date();
        // Sort by the absolute difference between endDate and today
        const sortedLeaseAgreements = data.sort((a, b) => {
          const diffA = Math.abs(new Date(a.endDate).getTime() - today.getTime());
          const diffB = Math.abs(new Date(b.endDate).getTime() - today.getTime());
          return diffA - diffB;
        });
        setLeaseAgreements(sortedLeaseAgreements);
      } catch (err) {
        console.error(err);
        setError("Failed to fetch lease agreements");
      } finally {
        setLoading(false);
      }
    };

    fetchLeaseAgreements();
  }, []);

  if (loading) return <p>Loading lease agreements...</p>;
  if (error) return <p>{error}</p>;

  const filteredAgreements = leaseAgreements.filter((agreement) =>
    agreement.endDate.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const indexOfLastAgreement = currentPage * agreementsPerPage;
  const indexOfFirstAgreement = indexOfLastAgreement - agreementsPerPage;
  const currentAgreements = filteredAgreements.slice(indexOfFirstAgreement, indexOfLastAgreement);
  const totalPages = Math.ceil(filteredAgreements.length / agreementsPerPage);

  return (
    <div className="min-h-screen bg-background flex flex-col items-center p-6">
      <h2 className="text-2xl font-medium mb-6">Lease Agreements</h2>

      {/* Search Bar */}
      <div className="relative w-full max-w-md mb-6">
        <input
          type="text"
          placeholder="Search by End Date"
          value={searchTerm}
          onChange={(e) => {
            setSearchTerm(e.target.value);
            setCurrentPage(1);
          }}
          className="w-full p-4 pl-12 border border-gray-300 rounded-full bg-white shadow-md focus:outline-none focus:ring-2 focus:ring-gray-400"
        />
        <span className="absolute left-4 top-1/2 transform -translate-y-1/2 text-gray-500">
          <Search className="w-5 h-5" />
        </span>
      </div>

      {/* Lease Agreement List */}
      <div className="w-full max-w-md bg-white shadow-lg rounded-lg overflow-hidden">
        {currentAgreements.map((agreement, index) => (
          <div
            key={agreement.id}
            className={`flex items-center justify-between p-4 ${index % 2 === 0 ? "bg-white" : "bg-[#F0F4F3]"
              }`}
          >
            {/* Agreement Info */}
            <div>
              <p className="font-medium text-lg">Lease #{agreement.id}</p>
              <p className="text-gray-500 text-sm">End Date: {agreement.endDate}</p>
              <p className="text-gray-500 text-sm">Status: {agreement.status.name}</p>
            </div>

            {/* Manage Button */}
            <MainButton
              onClick={() => navigate(`/admin/manageTenant/${agreement.unitUser.userId}`)}
              className="bg-black text-white px-4 py-1.5 rounded-full text-sm !m-0"
            >
              Manage
            </MainButton>
          </div>
        ))}
      </div>

      {/* Pagination Controls */}
      {totalPages > 1 && (
        <div className="flex space-x-4 mt-6">
          <button
            onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
            disabled={currentPage === 1}
            className="text-gray-500 hover:text-black disabled:opacity-50"
          >
            Previous
          </button>

          {Array.from({ length: totalPages }, (_, index) => index + 1).map((page) => (
            <button
              key={page}
              onClick={() => setCurrentPage(page)}
              className={`w-8 h-8 flex items-center justify-center rounded-full font-medium ${currentPage === page
                ? "bg-black text-white"
                : "text-gray-600 hover:text-black"
                }`}
            >
              {page}
            </button>
          ))}

          <button
            onClick={() => setCurrentPage((prev) => (prev < totalPages ? prev + 1 : prev))}
            disabled={currentPage >= totalPages}
            className="text-gray-500 hover:text-black disabled:opacity-50"
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
}

export default AdminManageLease
