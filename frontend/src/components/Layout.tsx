import React, { useState } from 'react';
import SideNavbar from './navbar/SideNavbar';
import { Outlet } from 'react-router-dom';
import { Menu } from 'lucide-react';

const MainLayout = () => {
  const [isSidebarVisible, setIsSidebarVisible] = useState(false);

  const toggleSidebar = () => {
    setIsSidebarVisible(!isSidebarVisible);
  };

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
      <button
        onClick={toggleSidebar}
        className={`fixed p-2 rounded-lg z-50 md:hidden ${
          isSidebarVisible ? "text-white ml-64" : "text-primary ml-4"
        } transition-all duration-300 ease-in-out`}
      >
        <Menu size={24} />
      </button>

      {/* Sidebar */}
      <aside
        className={`fixed inset-y-0 left-0 w-64 bg-gray-800 text-white transform transition-transform duration-300 ease-in-out z-40 ${
          isSidebarVisible ? 'translate-x-0' : '-translate-x-full'
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