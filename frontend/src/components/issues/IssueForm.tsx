import React, { useEffect, useState } from "react";
import FormInput from "../form/FormInput";
import MainButton from "../MainButton";
import FormSelect from "../form/FormSelect";
import { getData } from "../../services/api";

const IssueReportForm: React.FC = () => {
  const [issueType, setIssueType] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [dropdownOptions, setDropdownOptions] = useState<{ value: string; label: string }[]>([]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log({ issueType, description,});
  };


  useEffect(() => {
    async function fetchIssueTypes() {
      const response = await getData<{ id: number; name: string }[]>("issues/types");
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
            onChange={(e) => setIssueType(e.target.value)} options={dropdownOptions}    
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
