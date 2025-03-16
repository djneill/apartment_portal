import Login from "./components/login/Login";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "./components/Home/Home";
import UserProfile from "./components/UserProfile";
import "./App.css";
import {
  FormDemo,
  TenantDashboard,
} from "./pages";
import ManageGuests from "./pages/ManageGuests";
// import ModalExample from "./components/ModalExample";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/home" element={<Home />} />
        <Route path="/users/:id" element={<UserProfile />} />
        <Route path="/formdemo" element={<FormDemo />} />
        <Route path="/guests" element={<ManageGuests />} />
        <Route path="/tenantdashboard" element={<TenantDashboard />} />
        {/* <Route path="/admindashboard" element={<AdminDashboard />} /> */}
        {/* <Route path="/modal-example" element={<ModalExample />} /> */}
      </Routes>
    </BrowserRouter>
  );
}

export default App;
