import React, { useState } from 'react';
import SideNavbar from './navbar/SideNavbar';
import { Outlet } from 'react-router-dom';
import { Menu } from 'lucide-react'; 

const MainLayout = () => {
  const [isSidebarVisible, setIsSidebarVisible] = useState(true);

  const toggleSidebar = () => {
    setIsSidebarVisible(!isSidebarVisible);
  };

  return (
    <div className="flex">
      <button
        onClick={toggleSidebar}
        className="fixed top-4 left-4 p-2 text-white bg-gray-800 rounded-lg z-50"
      >
        <Menu size={24} />
      </button>

      <aside
        className={`fixed inset-y-0 left-0 w-64 bg-gray-800 text-white transform transition-transform duration-300 ease-in-out z-40 ${
          isSidebarVisible ? 'translate-x-0' : '-translate-x-full'
        } md:translate-x-0 md:relative`}
      >
        <SideNavbar />
      </aside>

      <main className="flex-1 p-4 overflow-y-auto">
        <Outlet />
      </main>
    </div>
  );
};

export default MainLayout;