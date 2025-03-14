import SideNavbar from './navbar/SideNavbar';
import { Outlet } from 'react-router-dom';

const MainLayout = () => {
  return (
    <div className="flex">
      <aside className="w-64 bg-gray-800 text-white">
        <SideNavbar />
      </aside>
      <main className="flex-1 p-4 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  );
};

export default MainLayout;