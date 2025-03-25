import React, { useEffect, useState } from "react";
import FormInput from "../form/FormInput";
import MainButton from "../MainButton";
import FormSelect from "../form/FormSelect";
import { getData, postData } from "../../services/api";
import useGlobalContext from "../../hooks/useGlobalContext";
import { useToast } from "../ToastProvider";

const IssueReportForm: React.FC = () => {
  const [issueType, setIssueType] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [dropdownOptions, setDropdownOptions] = useState<
    { value: string; label: string }[]
  >([]);
  const { user: globalUser } = useGlobalContext();
  const { addToast } = useToast()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const requestBody = {
      userId: globalUser?.userId,
      issueTypeId: parseInt(issueType),
      description: description,
    };
    try {
      const response = await postData("Issues/report", requestBody);
      console.log("Issue reported successfully:", response);

      setIssueType("");
      setDescription("");

      addToast("Successfully reported issue", {
        type: "success",
        duration: 3000,
      });


    } catch (error) {
      addToast("Error submitting issue", {
        type: "error",
        duration: 3000,
      });


      console.error("Failed to report issue:", error);
    }
  };

  useEffect(() => {
    async function fetchIssueTypes() {
      const response =
        await getData<{ id: number; name: string }[]>("issues/Types");
      if (response) {
        const options = response.map((issue) => ({
          value: issue.id.toString(),
          label: issue.name,
        }));
        setDropdownOptions(options);
      }
    }

    fetchIssueTypes();
  }, []);

  return (
    <form
      onSubmit={handleSubmit}
      className="w-full items-center p-5 text-sm font-medium bg-white rounded-2xl"
    >
      <div>
        <FormSelect
          label="Type of Issue"
          value={issueType}
          onChange={(e) => setIssueType(e.target.value)}
          options={dropdownOptions}
          required
        />

        <FormInput
          label="Description of Issue"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Write a detailed description"
          required
        />
      </div>

      <MainButton
        type="submit"
        className="px-8 py-3 mt-9 w-full text-white whitespace-nowrap rounded-3xl bg-neutral-700"
      >
        Submit
      </MainButton>
    </form>
  );
};

export default IssueReportForm;
