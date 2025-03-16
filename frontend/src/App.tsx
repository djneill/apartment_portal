import { BrowserRouter, Routes, Route } from "react-router-dom";
import Login from "./pages/shared/Login";
import Home from "./pages/shared/Home";
import UserProfile from "./components/UserProfile";
import ReportIssue from "./pages/guest/ReportIssue";
import "./App.css";
import Layout from "./components/Layout";
import { FormDemo, TenantDashboard } from "./pages";
import ManageGuests from "./pages/ManageGuests";
// import ModalExample from "./components/ModalExample";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/reportissue" element={<ReportIssue />} />
          <Route path="/users/:id" element={<UserProfile />} />
          <Route path="/formdemo" element={<FormDemo />} />
          <Route path="/guests" element={<ManageGuests />} />
          <Route path="/tenantdashboard" element={<TenantDashboard />} />
          <Route path="/admindashboard" element={<TempAdminDashboard />} />
          {/* <Route path="/modal-example" element={<ModalExample />} /> */}
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
