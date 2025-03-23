import React from "react";

interface LeasePreviewProps {
  previewImageUrl: string;
  pdfUrl: string;
  containerWidth?: string;
  containerHeight?: string;
}

const LeasePreview: React.FC<LeasePreviewProps> = ({
  previewImageUrl,
  pdfUrl,
  containerWidth = "300px",
  containerHeight = "320px",
}) => {
  return (
    <div
      style={{ width: containerWidth, height: containerHeight }}
      className="bg-white rounded-lg overflow-hidden shadow-md flex items-center justify-center"
    >
      <a
        href={pdfUrl}
        target="_blank"
        rel="noopener noreferrer"
        className="w-full h-full block"
      >
        <img
          src={previewImageUrl}
          alt="Lease Renewal Preview"
          className="w-full h-full object-contain cursor-pointer"
        />
      </a>
    </div>
  );
};

export default LeasePreview;
