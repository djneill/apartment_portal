import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import GlobalContext from "./hooks/GlobalContextProvider";
import ToastProvider from "./components/ToastProvider.tsx";
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <GlobalContext>
      <ToastProvider>
        <BrowserRouter>
          <App />
        </BrowserRouter>
      </ToastProvider>
    </GlobalContext>
  </StrictMode>,
);
