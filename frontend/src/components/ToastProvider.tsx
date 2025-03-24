import React, {
  createContext,
  useState,
  useCallback,
  useMemo,
  useContext,
} from "react"
import { ToastOptions, Toast } from "../Types";
import ToastContainer from "./ToastContainer";

interface ToastContextProps {
  addToast: (message: string, options?: ToastOptions) => void;
  removeToast: (id: number) => void;
}

const ToastContext = createContext<ToastContextProps | undefined>(undefined);

export const useToast = () => {
  const context = useContext(ToastContext);
  if (!context) {
    throw new Error("useToast must be used within a ToastProvider");
  }
  return context;
};

export const ToastProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {

  const [toasts, setToasts] = useState<Toast[]>([]);

  const addToast = useCallback(
    (message: string, options: ToastOptions = {}) => {
      if (!message) {
        console.error("Toast message is empty");
        return;
      }
      const { type = "info", duration = 3000 } = options;


      const id = Date.now();
      const newToast: Toast = { id, message, type, duration };

      // add the new toast to toast array
      setToasts((prev) => [...prev, newToast]);

      //remove toast after duration ends
      setTimeout(() => {
        setToasts((prev) => prev.filter((toast) => toast.id !== id));
      }, duration);
    },
    []
  );

  // Remove a toast manually.
  const removeToast = useCallback((id: number) => {
    setToasts((prev) => prev.filter((toast) => toast.id !== id));
  }, []);

  //memoize the  
  const contextValue = useMemo(() => ({ addToast, removeToast }), [
    addToast,
    removeToast,
  ]);



  return (
    <ToastContext.Provider value={contextValue}>
      {children}
      {/* Render Toasts via a Portal */}
      <ToastContainer toasts={toasts} removeToast={removeToast} />
    </ToastContext.Provider>
  );
}
export default ToastProvider
