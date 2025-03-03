import React from "react";

interface SignInButtonProps {
  onClick?: () => void;
}

const SignInButton: React.FC<SignInButtonProps> = ({ onClick }) => {
  return (
    <button
      type="submit"
      onClick={onClick}
      className="self-center px-16 py-5 mt-11 w-full text-xl font-semibold text-white rounded-xl bg-primary-700  cursor-pointer"
    >
      Sign In
    </button>
  );
};

export default SignInButton;
