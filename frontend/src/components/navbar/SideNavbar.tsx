import * as React from 'react';
import { Link, useLocation } from 'react-router-dom';
import {
  LayoutDashboard,
  Users,
  AlertCircle,
  Brain,
  Moon,
  Settings,
  LogOut,
} from 'lucide-react';

interface NavItemProps {
  icon: React.ReactNode;
  label: string;
  to: string;
  count?: number;
}

const SideNavbar = () => {
  const location = useLocation();

  const user = {
    avatarSrc:
      'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
    userName: 'Jane Doe',
  };

  const navItems = [
    { icon: <LayoutDashboard size={20} />, label: 'Dashboard', to: '/home' },
    { icon: <Users size={20} />, label: 'Manage Tenants', to: '/users/1' },
    { icon: <AlertCircle size={20} />, label: 'Manage Issues', to: '/reportissue' },
    { icon: <Brain size={20} />, label: 'AI Insights', to: '/formdemo' },
  ];

  const settingsItems = [
    { icon: <Moon size={20} />, label: 'Dark Mode', to: '/darkmode' }, 
    { icon: <Settings size={20} />, label: 'Settings', to: '/settings' },
    { icon: <LogOut size={20} />, label: 'Log Out', to: '/logout' },
  ];

  return (
    <nav className="flex flex-col items-start pt-14 pr-6 pb-28 mx-auto w-full font-medium bg-primary max-w-[480px] h-full">
      {/* User Profile */}
      <div className="flex gap-3 items-center mt-12 ml-4 text-xl text-white">
        <img
          src={user.avatarSrc}
          alt={`${user.userName}'s avatar`}
          className="object-contain shrink-0 self-stretch my-auto aspect-square rounded-full w-[75px]"
        />
        <h2 className="self-stretch my-auto rounded-none w-[95px]">
          {user.userName}
        </h2>
      </div>

      {/* Navigation Items */}
      <section className="mt-16 w-full">
        {navItems.map((item, index) => (
          <NavItem
            key={index}
            icon={item.icon}
            label={item.label}
            to={item.to}
            isActive={location.pathname === item.to || (item.to === '/users/1' && location.pathname.startsWith('/users'))}
          />
        ))}
      </section>

      {/* Settings and Dark Mode */}
      <section className="flex flex-col items-start mt-40 ml-4 max-w-full text-xl text-white w-full">
        {settingsItems.map((item, index) => (
          <NavItem
            key={index}
            icon={item.icon}
            label={item.label}
            to={item.to}
            isActive={location.pathname === item.to}
          />
        ))}
      </section>
    </nav>
  );
};

const NavItem: React.FC<NavItemProps & { isActive?: boolean }> = ({
  icon,
  label,
  to,
  count,
  isActive = false,
}) => {
  return (
    <Link to={to}>
      <div
        className={`flex justify-between items-center px-6 py-3 mt-3 w-full text-xl ${
          isActive ? 'text-black bg-secondary' : 'text-white'
        } rounded-3xl`}
      >
        <div className="flex gap-4 items-center whitespace-nowrap">
          {icon}
          <span>{label}</span>
        </div>
        {count !== undefined && (
          <div className="px-3 py-1 text-base text-black rounded-xl ">
            {count}
          </div>
        )}
      </div>
    </Link>
  );
};

export default SideNavbar;