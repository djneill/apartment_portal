import React, { useState } from "react";

interface SignatureFieldProps {
  fullName: string;
}

const LeaseSignature: React.FC<SignatureFieldProps> = ({ fullName }) => {
  const [signed, setSigned] = useState(false);

  const handleClick = () => {
    if (!signed) {
      setSigned(true);
    }
  };

  return (
    <div className="space-y-2">
      <div
        onClick={handleClick}
        className="border-b-2 border-black w-full cursor-pointer pb-1"
      >
        {signed ? (
          <span
            style={{ fontFamily: '"Great Vibes", cursive' }}
            className="text-2xl"
          >
            {fullName}
          </span>
        ) : (
          <span className="text-dark-gray">Tap to sign</span>
        )}
      </div>
    </div>
  );
};

export default LeaseSignature;
