import { useState } from "react";
import SideNavbar from "./navbar/SideNavbar";
import { Navigate, Outlet, useLocation } from "react-router-dom";
import { Menu } from "lucide-react";
import useGlobalContext from "../hooks/useGlobalContext";

const MainLayout = ({ usersRole }: { usersRole: string }) => {
  const { user } = useGlobalContext();

  const location = useLocation()

  const [isSidebarVisible, setIsSidebarVisible] = useState(false);

  const toggleSidebar = () => {
    setIsSidebarVisible(!isSidebarVisible);
  };

  if (user?.userId === 0) return <Navigate to="/" replace />;
  if (user?.roles?.includes("Admin") && usersRole !== "Admin")
    return <Navigate to="/admindashboard" replace />;
  if (user?.roles?.includes("Tenant") && usersRole !== "Tenant")
    return <Navigate to="/tenantdashboard" replace />;


  //disable the menu btn on specific routes
  const disabledRoutes = ["/admin/manageTenant",];
  const showMenuButton = !disabledRoutes.includes(location.pathname);

  return (
    <div className="flex relative">
      {/* Overlay for mobile sidebar */}
      {isSidebarVisible && (
        <div
          className="fixed inset-0 bg-black/30 bg-opacity-50 z-30 md:hidden"
          onClick={toggleSidebar}
        />
      )}

      {/* Menu Button */}

      {showMenuButton &&
        <button
          onClick={toggleSidebar}
          className={`fixed pt-6 rounded-lg z-50 md:hidden ${isSidebarVisible ? "text-white" : "text-primary"
            } ml-4`}
        >
          <Menu size={32} />
        </button>
      }

      {/* Sidebar */}
      <aside
        className={`fixed inset-y-0 left-0 w-64 transform transition-transform duration-300 ease-in-out z-40 ${isSidebarVisible ? "translate-x-0" : "-translate-x-full"
          } md:translate-x-0 md:relative`}
      >
        <SideNavbar />
      </aside>

      {/* Main Content */}
      <main className="flex-1 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  );
};

export default MainLayout;
