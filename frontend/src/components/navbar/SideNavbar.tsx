import {  useLocation } from 'react-router-dom';
import {
  LayoutDashboard,
  Home,
  Users,
  AlertCircle,
  Brain,
  Moon,
  Settings,
  LogOut,
} from 'lucide-react';
import NavItem from './NavItem';


const SideNavbar = () => {
  const location = useLocation();

  const user = {
    avatarSrc:
      'https://www.gravatar.com/avatar/2c7d99fe281ecd3bcd65ab915bac6dd5?s=250',
    userName: 'Jane Doe',
    apartment: 'Unit 205',
  };

  const navItems = [
    { icon: <Home size={20} />, label: 'Dashboard', to: '/home' },
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
    <nav className="flex flex-col items-start pt-14 mx-auto w-full font-medium bg-primary max-w-[480px] h-full">
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
        <p className="text-sm">
          {user.apartment}
        </p>
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
            isActive={location.pathname === item.to || (item.to === '/users/1' && location.pathname.startsWith('/users'))}
          />
        ))}
      </section>

      {/* Settings and Dark Mode */}
      <section className="flex flex-col items-start mt-40 ml-4 max-w-full text-xl text-white ">
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

export default SideNavbar;