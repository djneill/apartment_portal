import axios from "axios";

const BASE_URL = import.meta.env.VITE_API;

export const login = async (email: string, password: string) => {
  try {
    const response = await axios.post(
      `${BASE_URL}login?useCookies=true&useSessionCookies=true`,
      {
        email,
        password,
      },
      {
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
        },
        withCredentials: true,
      }
    );

    return response;
  } catch (error) {
    console.error("Login error:", error);
    throw error;
  }
};

export const getUserRoles = async (): Promise<string[]> => {
  try {
    const response = await axios.get<string[]>(`${BASE_URL}Users/roles`, {
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
      withCredentials: true,
    });
    return response.data;
  } catch (error) {
    console.error("Error fetching roles:", error);
    throw error;
  }
};
