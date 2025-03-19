import React from "react";

interface SignInButtonProps {
  onClick?: () => void;
  text: string;
}

const SignInButton: React.FC<SignInButtonProps> = ({ onClick, text }) => {
  return (
    <button
      type="submit"
      onClick={onClick}
      className="self-center px-16 py-5 mt-11 w-full text-xl font-semibold text-white rounded-xl bg-primary-700 cursor-pointer"
    >
      {text}
    </button>
  );
};

export default SignInButton;