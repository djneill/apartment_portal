import React, { useState } from "react";

interface FormDropdownProps {
  label: string;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  options?: Array<{ value: string; label: string }>;
}

const FormDropdown: React.FC<FormDropdownProps> = ({
  label,
  value,
  onChange,
  placeholder = "Select an option",
  options = [],
}) => {
  const [isOpen, setIsOpen] = useState(false);

  const handleSelect = (selectedValue: string) => {
    onChange(selectedValue);
    setIsOpen(false);
  };

  const dropdownOptions =
    options.length > 0
      ? options
      : [
          { value: "bug", label: "Bug" },
          { value: "feature", label: "Feature Request" },
          { value: "support", label: "Support Request" },
          { value: "other", label: "Other" },
        ];

  return (
    <div className="flex flex-col w-full">
      <label className="self-start text-neutral-500">{label}</label>

      <div className="relative">
        <button
          type="button"
          onClick={() => setIsOpen(!isOpen)}
          className="flex gap-5 justify-between w-full py-px pr-2 mt-4 text-black bg-white border-b border-neutral-300"
          aria-haspopup="listbox"
          aria-expanded={isOpen}
        >
          <span>{value || placeholder}</span>
          <img
            src="https://cdn.builder.io/api/v1/image/assets/TEMP/536802cfa4222515ec58c3398ef195753ea444acbecd94dcad25808ccc6aa8f8?placeholderIfAbsent=true&apiKey=6698034c433049d49c30c576e164363d"
            alt="Dropdown arrow"
            className="object-contain shrink-0 self-start mt-0 aspect-square w-[25px]"
          />
        </button>

        {isOpen && (
          <ul
            className="absolute z-10 w-full mt-1 bg-white border border-neutral-300 rounded shadow-lg"
            role="listbox"
          >
            {dropdownOptions.map((option) => (
              <li
                key={option.value}
                role="option"
                aria-selected={value === option.value}
                onClick={() => handleSelect(option.value)}
                className="px-4 py-2 cursor-pointer hover:bg-neutral-100"
              >
                {option.label}
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  );
};

export default FormDropdown;
