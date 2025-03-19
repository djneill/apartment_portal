import { Routes, Route, redirect } from "react-router-dom";
import Login from "./pages/shared/Login";

import UserProfile from "./components/UserProfile";
import ReportIssue from "./pages/guest/ReportIssue";
import "./App.css";
import Layout from "./components/Layout";
import { TenantDashboard } from "./pages";
import ManageGuests from "./pages/ManageGuests";

import AdminDashboard from "./pages/AdminDashboard";
import { useEffect } from "react";
import { CurrentUserResponseType } from "./Types";
import { getData } from "./services/api";
import useGlobalContext from "./hooks/useGlobalContext";
import { getUserRoles } from "./services/auth";

function App() {
  const { setUser } = useGlobalContext();

  useEffect(() => {
    (async () => {
      const currentUserResponse = await getData<CurrentUserResponseType>(
        "users/currentuser"
      );

      if (!currentUserResponse.id) {
        redirect("/");
        return;
      }

      const roles = await getUserRoles();

      setUser({
        userId: currentUserResponse.id,
        userName: currentUserResponse.userName,
        firstName: currentUserResponse.firstName,
        lastName: currentUserResponse.lastName,
        roles: roles,
      });
    })();
  }, [setUser]);

  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route element={<Layout />}>
        <Route>
          <Route path="/admindashboard" element={<AdminDashboard />} />
          <Route path="/users/:id" element={<UserProfile />} />
        </Route>
        <Route>
          <Route path="/guests" element={<ManageGuests />} />
          <Route path="/reportissue" element={<ReportIssue />} />
          <Route path="/tenantdashboard" element={<TenantDashboard />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
