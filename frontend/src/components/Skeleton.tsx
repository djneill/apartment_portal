const cn = (...classes: (string | undefined)[]) => {
  return classes.filter(Boolean).join(" ");
};

interface SkeletonItemProps {
  className?: string;
}

const SkeletonItem = ({ className }: SkeletonItemProps) => (
  <div
    className={cn(
      "bg-gray-200 animate-pulse",
      "rounded-lg",
      "p-4",
      "border border-gray-300",
      "flex flex-col space-y-2",
      className
    )}
  >
    <div className="h-5 bg-gray-300 rounded w-1/2 mb-2"></div>
    <div className="h-4 bg-gray-300 rounded w-full"></div>
    <div className="h-4 bg-gray-300 rounded w-3/4"></div>
  </div>
);

const Skeleton = () => {
  return (
    <div className="space-y-4 p-4 mt-12">
      <SkeletonItem />
      <SkeletonItem />
      <SkeletonItem />
      <SkeletonItem />
      <SkeletonItem />
    </div>
  );
};

export default Skeleton;
