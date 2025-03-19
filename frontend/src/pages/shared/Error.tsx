import { useNavigate } from "react-router-dom";
import { AlertCircle } from "lucide-react";
import SignInButton from "../../components/SignInButton";

function ErrorPage() {
  const navigate = useNavigate();

  return (
    <main className="flex items-center justify-center h-screen">
      <div className="text-center bg-white p-8 rounded-2xl shadow-lg max-w-md w-full mx-4">
        {/* Icon */}
        <div className="flex justify-center">
          <AlertCircle className="w-16 h-16 text-red-500" />
        </div>

        {/* Heading */}
        <h1 className="text-3xl font-bold text-primary-800 mt-6">
          Access Denied
        </h1>

        {/* Description */}
        <p className="text-gray-600 mt-4">
          Your account does not have a valid role. Please contact the
          administrator to resolve this issue.
        </p>

        {/* Button */}
        <SignInButton
          onClick={() => navigate("/")}
          text="Return to Login"
        />
      </div>
    </main>
  );
}

export default ErrorPage;