import { UserRound } from "lucide-react";

interface ProfileImageProps {
  src?: string;
  alt?: string;
  size?: "sm" | "md" | "lg";
  className?: string;
}

const ProfileImage = ({
  src,
  alt = "Profile",
  size = "md",
  className = "",
}: ProfileImageProps) => {
  const sizeStyles = {
    sm: "w-8 h-8",
    md: "w-12 h-12",
    lg: "w-16 h-16",
  };

  const iconSizes = {
    sm: 14,
    md: 20,
    lg: 28,
  };

  return (
    <div
      className={`${sizeStyles[size]} rounded-full overflow-hidden bg-white ${className}`}
    >
      {src ? (
        <img src={src} alt={alt} className="w-full h-full object-cover" />
      ) : (
        <div className="w-full h-full flex items-center justify-center bg-white text-primary">
          <UserRound size={iconSizes["lg"]} />
        </div>
      )}
    </div>
  );
};

export default ProfileImage;
