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
  const { user: globalUser } = useGlobalContext();

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

  const navItems = [
    { icon: <Home size={20} />, label: "Dashboard", to: getDashboardPath() },
    { icon: <Users size={20} />, label: "Manage Tenants", to: "/guests" },
    {
      icon: <AlertCircle size={20} />,
      label: "Manage Issues",
      to: "/reportissue",
    },
    { icon: <Brain size={20} />, label: "AI Insights", to: "/guests" },
  ];

  const settingsItems = [
    { icon: <Moon size={20} />, label: "Dark Mode", to: "/darkmode" },
    { icon: <Settings size={20} />, label: "Settings", to: "/settings" },
    { icon: <LogOut size={20} />, label: "Log Out", to: "/logout" },
  ];

  return (
    <nav className="flex flex-col items-start pt-14 mx-auto w-full font-medium bg-primary max-w-[480px] min-h-screen">
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

      {/* Settings and Dark Mode */}
      <section className="flex flex-col items-start mt-20 ml-4 text-xl text-white ">
        {settingsItems.map((item, index) => (
          <NavItem
            key={index}
            icon={item.icon}
            label={item.label}
            to={item.to}
            isActive={location.pathname === item.to}
          />
        ))}
        <button
          type="button"
          onClick={async () => {
            console.log("Logout");
            await postData("logout", null);
            navigate("/");
          }}
        >
        </button>
      </section>
    </nav>
  );
};

export default SideNavbar;
