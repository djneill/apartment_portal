import React from "react";
interface SignatureFieldProps {
  fullName: string;
  isSigned: boolean;
  setIsSigned: React.Dispatch<React.SetStateAction<boolean>>;
}

const LeaseSignature: React.FC<SignatureFieldProps> = ({
  fullName,
  isSigned,
  setIsSigned,
}) => {
  const handleClick = () => {
    if (!isSigned) {
      setIsSigned(true);
    }
  };

  return (
    <div className="space-y-2">
      <div
        onClick={handleClick}
        className="border-b-2 border-black w-full cursor-pointer pb-1"
      >
        {isSigned ? (
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
