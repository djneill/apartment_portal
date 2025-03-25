import { User } from "../Types";
import FormInput from "../components/form/FormInput";
import FormPhoneInput from "../components/form/FormPhoneInput";
import {
  FormDateInput,
  FormEmailInput,
  FormPasswordInput,
} from "../components/form";
import useGlobalContext from "../hooks/useGlobalContext";
import { postData } from "../services/api";
import { useState } from "react";
import { useToast } from "../components/ToastProvider";

const RegisterTenant = () => {

  const { addToast } = useToast();

  const user = useGlobalContext().user as User;
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    dateOfBirth: "",
    email: "",
    password: "",
    startDate: "",
    endDate: "",
    unitNumber: "",
  });

  const [errors, setErrors] = useState({
    firstName: "",
    lastName: "",
    phoneNumber: "",
    dateOfBirth: "",
    email: "",
    password: "",
    startDate: "",
    endDate: "",
    unitNumber: "",
  });

  if (!user?.roles?.includes("Admin")) {
    return <p>Access Denied</p>;
  }

  const handleChange = (key: string, value: string) => {
    setFormData((prev) => ({ ...prev, [key]: value }));
    setErrors((prev) => ({ ...prev, [key]: "" }));
  };

  const validate = () => {
    const newErrors = {
      firstName: formData.firstName ? "" : "First Name is required",
      lastName: formData.lastName ? "" : "Last Name is required",
      phoneNumber: formData.phoneNumber ? "" : "Phone Number is required",
      dateOfBirth: formData.dateOfBirth ? "" : "Date of Birth is required",
      email: formData.email ? "" : "Email is required",
      password: formData.password ? "" : "Password is required",
      startDate: formData.startDate ? "" : "Start Date is required",
      endDate: formData.endDate ? "" : "End Date is required",
      unitNumber: formData.unitNumber ? "" : "Unit Number is required",
    };

    if (formData.startDate && formData.endDate) {
      const start = new Date(formData.startDate);
      const end = new Date(formData.endDate);

      if (start > end) {
        newErrors.endDate = "End date must be after start date";
      }
    }

    setErrors(newErrors);
    return Object.values(newErrors).every((err) => !err);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    try {
      const data = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        phoneNumber: formData.phoneNumber,
        dateOfBirth: formData.dateOfBirth,
        email: formData.email,
        password: formData.password,
        startDate: formData.startDate,
        endDate: formData.endDate,
        unitNumber: parseInt(formData.unitNumber),
        isPrimary: true,
      };

      await postData<User>("users/register", data);

      setFormData({
        firstName: "",
        lastName: "",
        phoneNumber: "",
        dateOfBirth: "",
        email: "",
        password: "",
        startDate: "",
        endDate: "",
        unitNumber: "",
      });

      addToast("Tenant was successfully registered!", {
        type: "success",
        duration: 3000,
      });


    } catch (error: any) {
      console.error("Error registering tenant:", error);

      addToast("Error registering tenant", {
        type: "error",
        duration: 3000,
      });

      const errorData = error.response?.data;

      if (Array.isArray(errorData)) {
        const passwordErrors = errorData
          .map((e: { description: string }) => e.description)
          .join(" ");

        setErrors((prev) => ({
          ...prev,
          password: passwordErrors,
        }));
      } else if (errorData?.message) {
        const message = errorData.message;

        if (message.toLowerCase().includes("unit")) {
          setErrors((prev) => ({ ...prev, unitNumber: message }));
        } else if (message.toLowerCase().includes("email")) {
          setErrors((prev) => ({ ...prev, email: message }));
        } else {
          setErrors((prev) => ({ ...prev, password: message }));
        }
      }
    }
  };

  return (
    <div className="p-4 md:p-6 bg-gray-100 min-h-screen">
      <h1 className="mt-12 mb-4 font-heading font-medium text-3xl">
        Register Tenant
      </h1>
      <form
        onSubmit={handleSubmit}
        className="w-full p-5 bg-white rounded-2xl flex flex-col"
      >
        <div className="p-4 md:p-6">
          <FormInput
            label="First Name"
            value={formData.firstName}
            onChange={(e) => handleChange("firstName", e.target.value)}
            error={errors.firstName}
            placeholder="Enter tenants's first name"
            required
            className="border-b-gray-300"
          />

          <FormInput
            label="Last Name"
            value={formData.lastName}
            onChange={(e) => handleChange("lastName", e.target.value)}
            error={errors.lastName}
            placeholder="Enter tenant's last name"
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

          <FormDateInput
            label="Date of Birth"
            value={formData.dateOfBirth}
            onChange={(value) => handleChange("dateOfBirth", value)}
            error={errors.dateOfBirth}
            placeholder="Enter date of birth"
            className="border-b-gray-300"
          />

          <FormEmailInput
            label="Email"
            value={formData.email}
            onChange={(value) => handleChange("email", value)}
            error={errors.email}
            placeholder="Enter email"
            required
            className="border-b-gray-300"
          />

          <FormPasswordInput
            label="Password"
            value={formData.password}
            onChange={(value) => handleChange("password", value)}
            error={errors.password}
            placeholder="Enter password"
            className="border-b-gray-300"
          />

          <FormDateInput
            label="Start Date"
            value={formData.startDate}
            onChange={(value) => handleChange("startDate", value)}
            error={errors.startDate}
            className="border-b-gray-300"
          />

          <FormDateInput
            label="End Date"
            value={formData.endDate}
            onChange={(value) => handleChange("endDate", value)}
            error={errors.endDate}
            className="border-b-gray-300"
          />

          <FormInput
            label="Unit Number"
            value={formData.unitNumber}
            onChange={(e) => handleChange("unitNumber", e.target.value)}
            error={errors.unitNumber}
            placeholder="Enter unit number"
            required
            className="border-b-gray-300"
          />

          <div className="md:mt-10 md:my-5">
            <button
              type="submit"
              className="w-full bg-primary text-white py-2 px-4 rounded-full hover:bg-primary/90 transition-colors"
            >
              Register Tenant
            </button>
          </div>
        </div>
      </form>
    </div>
  );
};

export default RegisterTenant;
