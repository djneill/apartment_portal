import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import fs from "fs";
import path from "path";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react(), tailwindcss()],
  build: {
    outDir: "../apartment_portal_api/apartment_portal_api/wwwroot",
    emptyOutDir: true,
  },
  server: {
    https: {
      key: fs.readFileSync(path.resolve(__dirname, "./localhost-key.pem")),
      cert: fs.readFileSync(path.resolve(__dirname, "./localhost.pem")),
    },
    host: "0.0.0.0",
  },
});
