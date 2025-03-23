import { useState } from "react";
import FormInput from "./FormInput";
import { Eye, EyeOff } from "lucide-react";

interface FormPasswordInputProps {
  label?: string;
  error?: string;
  value: string;
  onChange: (value: string) => void;
  className?: string;
  placeholder?: string;
}

const FormPasswordInput = ({
  label,
  error,
  value,
  onChange,
  className = "",
  placeholder = "",
  ...props
}: FormPasswordInputProps) => {
  const [showPassword, setShowPassword] = useState(false);

  return (
    <div className="relative">
      <FormInput
        type={showPassword ? "text" : "password"}
        label={label}
        error={error}
        value={value}
        placeholder={placeholder}
        onChange={(e) => onChange(e.target.value)}
        className={className}
        {...props}
      />
      <button
        type="button"
        onClick={() => setShowPassword(!showPassword)}
        className="absolute right-2 top-[38px] text-[#6B7280]"
      >
        {showPassword ? <EyeOff /> : <Eye />}
      </button>
    </div>
  );
};

export default FormPasswordInput;
