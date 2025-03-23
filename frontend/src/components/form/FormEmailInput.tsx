import FormInput from "./FormInput";

interface FormEmailInputProps {
  label?: string;
  error?: string;
  value: string;
  onChange: (value: string) => void;
  className?: string;
  placeholder?: string;
  required?: boolean;
}

const FormEmailInput = ({
  label,
  error,
  value,
  onChange,
  className = "",
  placeholder = "",
  ...props
}: FormEmailInputProps) => {
  return (
    <FormInput
      type="email"
      label={label}
      error={error}
      value={value}
      onChange={(e) => onChange(e.target.value)}
      className={className}
      placeholder={placeholder}
      required={props.required}
      {...props}
    />
  );
};

export default FormEmailInput;
