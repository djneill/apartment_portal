import { useState } from 'react';
import FormInput from "./FormInput";

interface FormPhoneInputProps {
    label?: string;
    error?: string;
    value: string;
    onChange: (value: string) => void;
    className?: string;
}

const FormPhoneInput = ({
    label,
    error,
    value,
    onChange,
    className = '',
    ...props
}: FormPhoneInputProps) => {
    const [localError, setLocalError] = useState<string | undefined>(undefined);

    const formatPhoneNumber = (input: string) => {
        const numbers = input.replace(/\D/g, '');

        if (numbers.length === 0) return '';

        if (numbers.length > 10) return `${numbers.slice(0, 3)}-${numbers.slice(3, 6)}-${numbers.slice(6, 10)}`;
        if (numbers.length >= 7) return `${numbers.slice(0, 3)}-${numbers.slice(3, 6)}-${numbers.slice(6)}`;
        if (numbers.length >= 4) return `${numbers.slice(0, 3)}-${numbers.slice(3)}`;
        return numbers;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const rawInput = e.target.value;
        const formatted = formatPhoneNumber(rawInput);

        if (formatted.length === 12 && formatted.split('-').join('').length === 10) {
            setLocalError(undefined);
        } else if (formatted.length > 0) {
            setLocalError('Phone number must be 10 digits');
        } else {
            setLocalError(undefined);
        }

        onChange(formatted);
    }

    return (
        <FormInput
            type='tel'
            label={label}
            error={error || localError}
            value={value}
            onChange={handleChange}
            placeholder='999-999-9999'
            maxLength={12}
            className={className}
            {...props}
        />
    );
};

export default FormPhoneInput;