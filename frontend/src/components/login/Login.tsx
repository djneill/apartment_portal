import * as React from "react";
import { useState } from "react";
import InputField from "../InputField";
import SignInButton from "../SignInButton";
import { User, Lock, Eye, EyeOff } from 'lucide-react';
import './Login.css'; 

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("enviar:", { username, password });
  };

  return (
    <main className="loginContainer">
      <form onSubmit={handleSubmit} className="loginForm">
        <h1 className="self-center">
          Hello Again!
        </h1>

        <InputField
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="username"
          icon={<User />}
          className="mt-8"
        />

        <InputField
          type={showPassword ? "text" : "password"}
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="password"
          icon={<Lock />}
          rightIcon={showPassword ? <EyeOff /> : <Eye />}
          onRightIconClick={() => setShowPassword(!showPassword)}
          className="mt-8"
        />

        <button
          type="button"
          className="self-end mt-2 text-xs font-medium"
        >
          Forgot Password ?
        </button>

        <SignInButton />
      </form>
    </main>
  );
}

export default Login;