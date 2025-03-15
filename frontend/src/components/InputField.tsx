import React from "react";

interface InputFieldProps {
  type: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  placeholder: string;
  icon: React.ReactNode; 
  rightIcon?: React.ReactNode; 
  onRightIconClick?: () => void;
  className?: string;
}

const InputField: React.FC<InputFieldProps> = ({
  type,
  value,
  onChange,
  placeholder,
  icon,
  rightIcon,
  onRightIconClick,
  className = "",
}) => {
  return (
    <div
      className={`flex gap-3 items-center px-2.5 py-3.5 whitespace-nowrap rounded-xl  ${className}`}
    >
      <div className="w-4 h-4">{icon}</div> 

      <input
        type={type}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        className="flex-auto w-full bg-transparent outline-none text-sm font-medium text-neutral-700 placeholder-neutral-700"
      />

      {rightIcon && (
        <button
          type="button"
          onClick={onRightIconClick}
          className="flex items-center justify-center w-4 h-4"
        >
          {rightIcon} 
        </button>
      )}
    </div>
  );
};

export default InputField;
