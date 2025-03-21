import { TextareaHTMLAttributes } from "react";

interface FormTextAreaProps
  extends TextareaHTMLAttributes<HTMLTextAreaElement> {
  label?: string;
  error?: string;
  rows?: number;
}

const FormTextArea = ({
  label,
  error,
  className = "",
  id,
  rows = 4,
  ...props
}: FormTextAreaProps) => {
  const inputId =
    id || (label ? label.toLowerCase().replace(/\s+/g, "-") : undefined);

  return (
    <div className="mb-4">
      {label && (
        <label htmlFor={inputId} className="block text-black text-left mb-2">
          {label}
        </label>
      )}
      <textarea
        id={inputId}
        rows={rows}
        className={`
                    w-full 
                    px-3
                    py-2 
                    bg-transparent 
                    text-black 
                    border 
                    border-primary 
                    rounded
                    focus:outline-none 
                    focus:border-primary 
                    placeholder:text-secondary 
                    ${className}`}
        {...props}
      />
      {error && <p className="mt-1 text-sm text-red-500">{error}</p>}
    </div>
  );
};

export default FormTextArea;
