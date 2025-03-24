import { Toast } from "../Types";
import ReactDOM from "react-dom";

interface ToastContainerProps {
  toasts: Toast[];
  removeToast: (id: number) => void;
}

interface ToastItemProps {
  toast: Toast;
  onClose: () => void;
}

//renders toast
const ToastItem: React.FC<ToastItemProps> = ({ toast, onClose }) => {
  let bgColor = "bg-blue-500";
  if (toast.type === "success") bgColor = "bg-green-500";
  else if (toast.type === "error") bgColor = "bg-red-500";
  else if (toast.type === "warning") bgColor = "bg-yellow-500";

  return (
    <div
      className={`${bgColor} text-white p-4 rounded shadow cursor-pointer`}
      onClick={onClose} // dismiss on click
    >
      {toast.message}
    </div>
  );
};

const ToastContainer: React.FC<ToastContainerProps> = ({
  toasts,
  removeToast,
}) => {
  //render toast outside of main dom hierarchy
  const portalElement =
    document.getElementById("toast-root") || createPortalRoot(); //if portal root doesn't exist create it

  return ReactDOM.createPortal(
    <div className="fixed top-4 right-4 space-y-2 z-50">
      {toasts.map((toast) => (
        <ToastItem
          key={toast.id}
          toast={toast}
          onClose={() => removeToast(toast.id)}
        />
      ))}
    </div>,
    portalElement,
  );
};

//creates a div so the toast can attach to it
function createPortalRoot(): HTMLElement {
  const root = document.createElement("div");
  root.id = "toast-root";
  document.body.appendChild(root);
  return root;
}

export default ToastContainer;
