import * as React from "react";
import { useState } from "react";
import InputField from "../../components/InputField";
import SignInButton from "../../components/SignInButton";
import { User, Lock, Eye, EyeOff } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { getUserRoles, login } from "../../services/auth";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await login(username, password);
      const roles = await getUserRoles();
      console.log("Roles:", roles);
      if (roles.includes("Admin")) {
        navigate("/admindashboard");
      } else if (roles.includes("Tenant")) {
        navigate("/tenantdashboard");
      } else {
        navigate("/home");
      }
    } catch (error) {
      console.error("Login failed:", error);
    }
  };

  return (
    <main className="loginContainer flex items-center justify-center h-screen">
      <form onSubmit={handleSubmit} className="loginForm">
        <h1 className="self-center text-center text-4xl leading-tight">Hello Again!</h1>

        <InputField
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="username"
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

        <SignInButton />
      </form>
    </main>
  );
}

export default Login;
