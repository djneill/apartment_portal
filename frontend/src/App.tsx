import React, { useState } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './pages/shared/Login';
import Home from './pages/shared/Home';
import UserProfile from './components/UserProfile';
import ReportIssue from './pages/guest/ReportIssue';
import FormDemo from './pages/FormDemo';
import AdminNav from './components/navbar/Narvbar'; 
import './App.css';

function App() {
  const [isNavOpen, setIsNavOpen] = useState(false);

  const toggleNav = () => {
    console.log('toggleNav');
    setIsNavOpen(!isNavOpen);
  };

  return (
    <BrowserRouter>
      <div>
        {/* Burger Menu Icon */}
        <div className="burger-menu" onClick={toggleNav}>
          <div className="burger-line"></div>
          <div className="burger-line"></div>
          <div className="burger-line"></div>
        </div>

        {/* Navbar Overlay */}
        <div className={`navbar-overlay ${isNavOpen ? 'open' : ''}`}>
          <AdminNav toggleNav={toggleNav} />
        </div>

        {/* Main Content */}
        <div className={`main-content ${isNavOpen ? 'shifted' : ''}`}>
          <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/home" element={<Home />} />
            <Route path="/reportissue" element={<ReportIssue />} />
            <Route path="/users/:id" element={<UserProfile />} />
            <Route path="/formdemo" element={<FormDemo />} />
          </Routes>
        </div>
      </div>
    </BrowserRouter>
  );
}

export default App;