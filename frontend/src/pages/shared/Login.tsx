import * as React from "react";
import { useState } from "react";
import InputField from "../../components/InputField";
import SignInButton from "../../components/SignInButton";
import { User, Lock, Eye, EyeOff } from "lucide-react";
import { Navigate, useNavigate } from "react-router-dom";
import { getUserRoles, login } from "../../services/auth";
import useGlobalContext from "../../hooks/useGlobalContext";
import { getData } from "../../services/api";
import { CurrentUserResponseType } from "../../types";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [errorMessage, setErrorMessage] = useState("");
  const navigate = useNavigate();
  const globalContext = useGlobalContext();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!username.trim() || !password.trim()) {
      setErrorMessage("Username and password are required.");
    }

    try {
      const response = await login(username, password);
      console.log("Login info:", response);

      if (response.status !== 200) {
        console.error("Login failed:", response);
        setErrorMessage("Login failed. Please check your email and password.");
        return;
      }

      const currentUserResponse = await getData<CurrentUserResponseType>(
        "users/currentuser"
      );

      const roles = await getUserRoles();
      console.log("Roles:", roles);

      globalContext.setUser({
        userId: currentUserResponse.id,
        userName: currentUserResponse.userName,
        firstName: currentUserResponse.firstName,
        lastName: currentUserResponse.lastName,
        roles: roles,
      });
      console.log("Current User:", currentUserResponse);

      if (roles.includes("Admin")) {
        navigate("/admindashboard");
      } else if (roles.includes("Tenant")) {
        navigate("/tenantdashboard");
      } else {
        navigate("/error");
      }
    } catch (error) {
      console.error("Login failed:", error);
      setErrorMessage("An unexpected error occurred. Please try again."); // Set error message for unexpected errors
    }
  };

  if (globalContext.user?.roles?.includes("Admin")) {
    return <Navigate to={"/admindashboard"} />;
  }

  if (globalContext.user?.roles?.includes("Tenant")) {
    return <Navigate to={"/tenantdashboard"} />;
  }

  return (
    <main className="loginContainer flex items-center justify-center h-screen">
      <form onSubmit={handleSubmit} className="loginForm">
        <h1 className="self-center text-center text-4xl leading-tight">
          Hello Again!
        </h1>

        <InputField
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="email"
          icon={<User />}
          className="mt-8 rounded-xl bg-zinc-300"
        />

        <InputField
          type={showPassword ? "text" : "password"}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="password"
          icon={<Lock />}
          rightIcon={showPassword ? <EyeOff /> : <Eye />}
          onRightIconClick={() => setShowPassword(!showPassword)}
          className="mt-8 rounded-xl bg-zinc-300"
        />

        <button type="button" className="self-end mt-2 text-xs font-medium">
          Forgot Password ?
        </button>

        <SignInButton text=" Sign In" />
        {/* Error message container */}
        {errorMessage && (
          <div className="text-red-500 text-sm">{errorMessage}</div>
        )}
      </form>
    </main>
  );
}

export default Login;
