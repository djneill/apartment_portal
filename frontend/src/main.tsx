import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import GlobalContext from "./hooks/GlobalContextProvider";
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <GlobalContext>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </GlobalContext>
  </StrictMode>
);
