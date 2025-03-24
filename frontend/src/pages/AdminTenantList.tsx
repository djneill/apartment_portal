import { useEffect, useState } from "react";
import { getData } from "../services/api";
import useGlobalContext from "../hooks/useGlobalContext";
import { useNavigate } from "react-router-dom";
import { ProfileImage } from "../tenantDashboard/components";
import MainButton from "../components/MainButton";
import { Search } from "lucide-react";
import Skeleton from "../components/Skeleton";

interface Tenant {
  id: number;
  firstName: string;
  lastName: string;
  unit: { number: number };
  status: { name: string };
}

const AdminTenantList = () => {
  const { user } = useGlobalContext();
  const navigate = useNavigate();
  const [tenants, setTenants] = useState<Tenant[]>([]);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [currentPage, setCurrentPage] = useState(1);
  const tenantsPerPage = 10;
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!user || !user?.roles?.includes("Admin")) {
      navigate("/");
      return;
    }

    const fetchTenants = async () => {
      try {
        const tenants = await getData<Tenant[]>("/users");
        const activeTenants = tenants.filter(
          (tenant) => tenant.status.name === "Active"
        );

        setTenants(activeTenants);
      } catch (error) {
        console.error("Failed to fetch tenants:", error);
        setError("Failed to fetch tenants");
      } finally {
        setLoading(false);
      }
    };

    fetchTenants();
  }, [user, navigate]);

  if (!user || !user?.roles?.includes("Admin")) {
    return <p>Access Denied</p>;
  }

  if (loading) return <Skeleton />;
  if (error) return <p>{error}</p>;

  const filteredTenants = tenants.filter((tenant) =>
    `${tenant.firstName} ${tenant.lastName}`
      .toLowerCase()
      .includes(searchTerm.toLowerCase())
  );

  const indexOfLastTenant = currentPage * tenantsPerPage;
  const indexOfFirstTenant = indexOfLastTenant - tenantsPerPage;
  const currentTenants = filteredTenants.slice(
    indexOfFirstTenant,
    indexOfLastTenant
  );
  const totalPages = Math.ceil(filteredTenants.length / tenantsPerPage);

  return (
    <div className="min-h-screen bg-background flex flex-col items-center p-6">
      <h2 className="text-2xl font-medium mb-6">Tenant List</h2>

      {/* Search Bar */}
      <div className="relative w-full max-w-md mb-6">
        <input
          type="text"
          placeholder="Search for Tenant"
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

      {/* Tenant List */}
      <div className="w-full max-w-md bg-white shadow-lg rounded-lg overflow-hidden">
        {currentTenants.map((tenant, index) => (
          <div
            key={tenant.id}
            className={`flex items-center justify-between p-4 ${
              index % 2 === 0 ? "bg-white" : "bg-[#F0F4F3]"
            }`}
          >
            {/* Tenant Info */}
            <div className="flex items-center space-x-3 flex-grow">
              <ProfileImage className="w-8 h-8" />
              <div>
                <p className="font-medium text-lg">
                  {tenant.firstName} {tenant.lastName}
                </p>
                <p className="text-gray-500 text-sm">
                  Unit {tenant.unit.number}
                </p>
              </div>
            </div>

            {/* Manage Button */}
            <MainButton
              onClick={() => navigate(`/admin/manageTenant/${tenant.id}`)}
              className="bg-black text-white px-4 py-1.5 rounded-full text-sm ml-auto"
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

          {/* Page Numbers */}
          {Array.from({ length: totalPages }, (_, index) => index + 1).map(
            (page) => (
              <button
                key={page}
                onClick={() => setCurrentPage(page)}
                className={`w-8 h-8 flex items-center justify-center rounded-full font-medium ${
                  currentPage === page
                    ? "bg-black text-white"
                    : "text-gray-600 hover:text-black"
                }`}
              >
                {page}
              </button>
            )
          )}

          {/* Next Button */}
          <button
            onClick={() =>
              setCurrentPage((prev) => (prev < totalPages ? prev + 1 : prev))
            }
            disabled={currentPage >= totalPages}
            className="text-gray-500 hover:text-black disabled:opacity-50"
          >
            Next
          </button>
        </div>
      )}
    </div>
  );
};

export default AdminTenantList;
