import { useLocation, useNavigate } from "react-router-dom";
import {
  Home,
  Users,
  AlertCircle,
  Brain,
  Moon,
  Settings,
  LogOut,
} from "lucide-react";
import NavItem from "./NavItem";
import { postData } from "../../services/api";
import useGlobalContext from "../../hooks/useGlobalContext";

const SideNavbar = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { user: globalUser, setUser } = useGlobalContext();

  const user = {
    avatarSrc:
      "https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250",
    apartment: globalUser?.unit?.unitNumber
      ? `Unit ${globalUser.unit.unitNumber}`
      : "",
    userName: globalUser
      ? `${globalUser.firstName} ${globalUser.lastName}`
      : "Guest",
  };

  const getDashboardPath = () => {
    if (!globalUser || !globalUser.roles) return "/";
    if (globalUser.roles.includes("Admin")) return "/admindashboard";
    if (globalUser.roles.includes("Tenant")) return "/tenantdashboard";
    return "/";
  };

  const handleLogout = async () => {
    await postData("logout", null);
    console.log("Logging out..."); 
    setUser(null); 
    navigate("/", { replace: true });
  }

  // items for Admin
  const adminNavItems = [
    { icon: <Home size={20} />, label: "Dashboard", to: "/admindashboard" },
    { icon: <Users size={20} />, label: "Manage Tenants", to: "/admin/tenantlist" },
    { icon: <AlertCircle size={20} />, label: "Manage Issues", to: "/issues" },
  ];

  //  items for Tenant
  const tenantNavItems = [
    { icon: <Home size={20} />, label: "Dashboard", to: "/tenantdashboard" },
    { icon: <Users size={20} />, label: "Manage Guests", to: "/guests" },
    { icon: <AlertCircle size={20} />, label: "Report Issue", to: "/reportissue" },
    { icon: <Brain size={20} />, label: "AI Insights", to: "/aiinsights" },
  ];

  // based on role 
  const navItems = globalUser?.roles?.includes("Admin")
    ? adminNavItems
    : tenantNavItems;

  return (
    <nav className="flex flex-col items-start pt-14 mx-auto font-medium bg-primary min-h-full ">
      {/* User Profile */}
      <div className="flex gap-3 items-center mt-12 ml-4 text-xl text-white">
        <img
          src={user.avatarSrc}
          alt={`${user.userName}'s avatar`}
          className="object-contain shrink-0 self-stretch my-auto aspect-square rounded-full w-[75px]"
        />
        <div>
          <h2 className="self-stretch my-auto rounded-none w-[95px]">
            {user.userName}
          </h2>
          <p className="text-sm">{user.apartment}</p>
        </div>
      </div>

      {/* Navigation Items */}
      <section className="flex flex-col items-start mt-10 ml-4 max-w-full text-xl text-white ">
        {navItems.map((item, index) => (
          <NavItem
            key={index}
            icon={item.icon}
            label={item.label}
            to={item.to}
            isActive={
              location.pathname === item.to ||
              (item.to === "/users/1" &&
                location.pathname.startsWith("/users")) ||
              ((item.to === "/admindashboard" ||
                item.to === "/tenantdashboard") &&
                (location.pathname === "/admindashboard" ||
                  location.pathname === "/tenantdashboard"))
            }
          />
        ))}
      </section>

      {/* Log Out Section */}
      <section className="mt-auto mb-10 ml-4 w-full">
        <button
          onClick={handleLogout}
          className="flex justify-between items-center px-6 py-3 mt-3 w-full text-xl text-white hover:bg-secondary hover:text-black rounded-3xl transition-colors duration-200"
        >
          <div className="flex gap-4 items-center whitespace-nowrap">
            <LogOut size={20} />
            <span>Log Out</span>
          </div>
        </button>
      </section>
    </nav>
  );
};

export default SideNavbar;