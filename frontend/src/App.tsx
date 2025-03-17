import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './pages/shared/Login';
import ReportIssue from './pages/guest/ReportIssue';
import './App.css';
import "./App.css";
import Layout from "./components/Layout";
import {  TenantDashboard } from "./pages";
import ManageGuests from "./pages/ManageGuests";
// import ModalExample from "./components/ModalExample";

function App() {
  return (
    <BrowserRouter>
      <Routes>
          <Route path="/" element={<Login />} />
          <Route element={<Layout />}>
            <Route path="/tenantdashboard" element={<TenantDashboard />} />
            <Route path="/reportissue" element={<ReportIssue />} />
            <Route path="/guests" element={<ManageGuests />} />
            <Route path="/admindashboard" element={<TempAdminDashboard />} />
          </Route>
        </Routes>
    </BrowserRouter>
  );
}

export default App;

const TempAdminDashboard = () => {
  return (
    <div>
      <h1>Admin Dashboard</h1>
    </div>
  );
};
