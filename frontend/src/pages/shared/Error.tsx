import { useNavigate } from "react-router-dom";
import { AlertCircle } from "lucide-react"; 

function ErrorPage() {
  const navigate = useNavigate();

  return (
    <main className="flex items-center justify-center h-screen ">
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
        <button
          onClick={() => navigate("/")}
          className="self-center px-16 py-5 mt-11 w-full text-xl font-semibold text-white rounded-xl bg-primary-700  cursor-pointer"
        >
          Return to Login
        </button>
      </div>
    </main>
  );
}

export default ErrorPage;