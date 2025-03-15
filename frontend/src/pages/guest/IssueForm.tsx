import React, { useState } from "react";
import FormDropdown from "../../components/form/FormDropdown";
import FormInput from "../../components/form/FormInput";
import MainButton from "../../components/MainButton";

const IssueReportForm: React.FC = () => {
  const [issueType, setIssueType] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [images, setImages] = useState<File[]>([]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log({ issueType, description, images });
  };

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      const fileArray = Array.from(e.target.files);
      setImages((prevImages) => [...prevImages, ...fileArray]);
    }
  };

  return (
    <form
      onSubmit={handleSubmit}
      className="flex gap-2.5 items-center px-5 py-5 text-sm font-medium bg-white rounded-2xl"
    >
      <section className="flex flex-col self-stretch pb-2 my-auto min-w-60 w-[315px]">
        <div>
          <FormDropdown
            label="Type of Issue"
            value={issueType}
            onChange={setIssueType}
            placeholder="Select an Issue Type"
          />

          <FormInput
            label="Description of Issue"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Write a detailed description"
          />
        </div>

        <label className="self-start mt-8 text-neutral-500">
          Upload Images (optional)
        </label>

        <div className="relative">
          <input
            type="file"
            accept="image/*"
            multiple
            onChange={handleImageUpload}
            className="absolute inset-0 opacity-0 cursor-pointer"
            aria-label="Upload images"
          />
          <button
            type="button"
            className="gap-2.5 self-start px-3.5 py-2 mt-2.5 text-black whitespace-nowrap bg-violet-300 rounded-3xl min-h-[30px]"
          >
            Upload
          </button>
        </div>

        <MainButton
          type="submit"
          className="px-8 py-3 mt-9 text-white whitespace-nowrap rounded-3xl bg-neutral-700"
        >
          Submit
        </MainButton>
      </section>
    </form>
  );
};

export default IssueReportForm;
