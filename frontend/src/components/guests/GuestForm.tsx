import { useState, useEffect } from "react";
import { ChevronUp } from "lucide-react";
import { FormInput, FormPhoneInput, FormSelect } from "../form";
import { GuestRequest, Guest } from "../../Types";

interface GuestFormProps {
  onSubmit: (data: GuestRequest) => void;
  editGuest?: Guest | null;
}

function formatPhoneNumber(phone: string): string {
  if (!phone) return "";
  const cleaned = phone.replace(/\D/g, "");
  if (cleaned.length === 10) {
    return `${cleaned.slice(0, 3)}-${cleaned.slice(3, 6)}-${cleaned.slice(6)}`;
  }
  return phone;
}

export default function GuestForm({ onSubmit, editGuest }: GuestFormProps) {
  const [formData, setFormData] = useState({
    firstName: editGuest?.firstName || "",
    lastName: editGuest?.lastName || "",
    phoneNumber: editGuest?.phoneNumber || "",
    duration: editGuest ? "4" : "4",
    carMake: "",
    carModel: "",
    licensePlate: "",
  });

  const [errors, setErrors] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    duration: "",
    carMake: "",
    carModel: "",
    licensePlate: "",
  });

  const [showParkingFields, setShowParkingFields] = useState(false);

  useEffect(() => {
    if (editGuest) {
      setFormData({
        firstName: editGuest.firstName || "",
        lastName: editGuest.lastName || "",
        phoneNumber: formatPhoneNumber(editGuest.phoneNumber) || "",
        duration: "4",
        carMake: "",
        carModel: "",
        licensePlate: "",
      });
    }
  }, [editGuest]);

  const handleToggleParking = () => {
    setShowParkingFields((prev) => !prev);
  };

  const parkingFields = showParkingFields && (
    <>
      <FormInput
        label="Car Make"
        value={formData.carMake || ""}
        onChange={(e) => handleChange("carMake", e.target.value)}
        error={errors.carMake}
        placeholder="Enter car make"
        className="border-b-gray-300"
      />
      <FormInput
        label="Car Model"
        value={formData.carModel || ""}
        onChange={(e) => handleChange("carModel", e.target.value)}
        error={errors.carModel}
        placeholder="Enter car model"
        className="border-b-gray-300"
      />
      <FormInput
        label="License Plate"
        value={formData.licensePlate || ""}
        onChange={(e) => handleChange("licensePlate", e.target.value)}
        error={errors.licensePlate}
        placeholder="Enter license plate"
        className="border-b-gray-300"
      />
    </>
  );

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({
      ...prev,
      [field]: field === "phoneNumber" ? formatPhoneNumber(value) : value,
    }));

    // Clears error when typing
    if (errors[field as keyof typeof errors]) {
      setErrors((prev) => ({
        ...prev,
        [field]: "",
      }));
    }
  };

  //NOT SECURE
  function generateAccessCode(): string {
    return Math.floor(100000 + Math.random() * 900000).toString();
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const newErrors = {
      firstName: !formData.firstName ? "First name is required" : "",
      lastName: !formData.lastName ? "Last name is required" : "",
      phoneNumber: !formData.phoneNumber ? "Phone number is required" : "",
      duration: !formData.duration ? "Duration is required" : "",
      carMake: "",
      carModel: "",
      carColor: "",
      licensePlate: "",
    };

    if (showParkingFields) {
      newErrors.carMake = !formData.carMake ? "Car make is required" : "";
      newErrors.carModel = !formData.carModel ? "Car model is required" : "";
      newErrors.licensePlate = !formData.licensePlate
        ? "License plate is required"
        : "";
    }

    setErrors(newErrors);

    //if no errors in array, call onSubmit function
    if (!Object.values(newErrors).some((error) => error)) {
      const parsedDuration = Number(formData.duration);

      const requestData: GuestRequest = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        phoneNumber: formData.phoneNumber,
        accessCode: generateAccessCode(),
        durationInHours: parsedDuration,
      };
      onSubmit(requestData);
      setFormData({
        firstName: "",
        lastName: "",
        phoneNumber: "",
        duration: "4",
        carMake: "",
        carModel: "",
        licensePlate: "",
      });
    }
  };

  const durationOptions = [
    { value: "4", label: "4 hours" },
    { value: "12", label: "12 hours" },
    { value: "24", label: "1 day" },
  ];

  return (
    <form
      onSubmit={handleSubmit}
      className="w-full p-5 bg-white rounded-2xl flex flex-col "
    >
      <FormInput
        label="First Name"
        value={formData.firstName}
        onChange={(e) => handleChange("firstName", e.target.value)}
        error={errors.firstName}
        placeholder="Enter guest's first name"
        required
        className="border-b-gray-300"
      />

      <FormInput
        label="Last Name"
        value={formData.lastName}
        onChange={(e) => handleChange("lastName", e.target.value)}
        error={errors.lastName}
        placeholder="Enter guest's last name"
        required
        className="border-b-gray-300"
      />

      <FormPhoneInput
        label="Phone Number"
        value={formData.phoneNumber}
        onChange={(value) => handleChange("phoneNumber", value)}
        error={errors.phoneNumber}
        className="border-b-gray-300"
      />

      <FormSelect
        label="Duration"
        value={formData.duration}
        onChange={(e) => handleChange("duration", e.target.value)}
        error={errors.duration}
        options={durationOptions}
        className="border-b-gray-300"
      />

      <div
        className="flex space-x-2 cursor-pointer  w-fit mb-4"
        onClick={handleToggleParking}
      >
        <p className="font-bold">Needs Parking?</p>
        <ChevronUp
          className={`transition-transform duration-300 ${
            showParkingFields ? "rotate-180" : "rotate-0"
          }`}
        />
      </div>

      {parkingFields}

      <div className="md:mt-10 md:my-5">
        <button
          type="submit"
          className="w-full bg-primary text-white py-2 px-4 rounded-full hover:bg-primary/90 transition-colors"
        >
          {editGuest ? "Update Guest" : "Add Guest"}
        </button>
      </div>
    </form>
  );
}
