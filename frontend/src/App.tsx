import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Login from './pages/shared/Login';
import ReportIssue from './pages/guest/ReportIssue';
import './App.css';
import "./App.css";
import Layout from "./components/Layout";
import {  TenantDashboard } from "./pages";
import ManageGuests from "./pages/ManageGuests";
import GlobalContext from "./hooks/GlobalContextProvider";
import AdminDashboard from "./pages/AdminDashboard";
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
            <Route path="/tenantdashboard" element={<TenantDashboard />} />
            <Route path="/admindashboard" element={<AdminDashboard />} />
          </Route>
        </Routes>
    </BrowserRouter>
  );
}

export default App;


