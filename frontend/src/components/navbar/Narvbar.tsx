import * as React from "react";
import {
  LayoutDashboard,
  Users,
  AlertCircle,
  Brain,
  Moon,
  Settings,
  LogOut,
} from "lucide-react";

interface NavItemProps {
  icon: React.ReactNode;
  label: string;
  count?: number;
  isActive?: boolean;
}


const AdminNav: React.FC = () => {
  const user = {
    avatarSrc:
      "https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250",
    userName: "Jane Doe",
  };

  const navItems = [
    { icon: <LayoutDashboard size={20} />, label: "Dashboard", count: 6 },
    { icon: <Users size={20} />, label: "Manage Tenants", isActive: true },
    { icon: <AlertCircle size={20} />, label: "Manage Issues" },
    { icon: <Brain size={20} />, label: "AI Insights" },
  ];

  const settingsItems = [
    { icon: <Settings size={20} />, label: "Settings" },
    { icon: <LogOut size={20} />, label: "Log Out" },
  ];

  return (
    <nav className="flex flex-col items-start pt-14 pr-6 pb-28 mx-auto w-full font-medium bg-neutral-700 max-w-[480px]">
      {/* Logo */}
      <img
        src="https://cdn.builder.io/api/v1/image/assets/TEMP/49730fa1139bf0c08f5601b20b9fce44cf9c3a23a7520274402396d5e11ba4b9?placeholderIfAbsent=true&apiKey=6698034c433049d49c30c576e164363d"
        alt="Company Logo"
        className="object-contain ml-4 w-9 aspect-square"
      />

      {/* User Profile */}
      <div className="flex gap-3 items-center mt-12 ml-4 text-xl text-white">
        <img
          src={user.avatarSrc}
          alt={`${user.userName}'s avatar`}
          className="object-contain shrink-0 self-stretch my-auto aspect-square rounded-[1000px] w-[75px]"
        />
        <h2 className="self-stretch my-auto rounded-none w-[95px]">
          {user.userName}
        </h2>
      </div>

      {/* Navigation Items */}
      <section className="mt-16 w-full">
        {navItems.map((item, index) => (
          <NavItem key={index} {...item} />
        ))}
      </section>

      {/* Dark Mode and Settings */}
      <section className="flex flex-col items-start mt-40 ml-4 max-w-full text-xl text-white w-[153px]">
        <div className="flex gap-4 items-center px-4 py-2 w-full rounded-3xl bg-gray-800">
          <Moon size={20} />
          <span>Dark Mode</span>
        </div>

        {settingsItems.map((item, index) => (
          <div key={index} className="flex gap-4 items-center mt-5">
            {item.icon}
            <span>{item.label}</span>
          </div>
        ))}
      </section>
    </nav>
  );
};

const NavItem: React.FC<NavItemProps> = ({
  icon,
  label,
  count,
  isActive = false,
}) => {
  return (
    <div
      className={`flex justify-between items-center px-4 py-3 mt-3 w-full text-xl ${
        isActive ? "text-black bg-gray-400" : "text-white"
      } rounded-3xl`}
    >
      <div className="flex gap-4 items-center">
        {icon}
        <span>{label}</span>
      </div>
      {count !== undefined && (
        <div className="px-3 py-1 text-base text-black rounded-xl bg-zinc-100">
          {count}
        </div>
      )}
    </div>
  );
};

export default AdminNav;