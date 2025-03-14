import axios from "axios";

const BASE_URL = import.meta.env.VITE_API;
//we can add the header for the bearer token here
const axiosInstance = axios.create({
  baseURL: `${BASE_URL}api/`,
  headers: {
    "Content-Type": "application/json",
  },
});

// GET
export const getData = async <T>(endpoint: string): Promise<T> => {
  const response = await axiosInstance.get<T>(endpoint);
  return response.data;
};

// POST
export const postData = async <T>(
  endpoint: string,
  payload: unknown
): Promise<T> => {
  const response = await axiosInstance.post<T>(endpoint, payload);
  return response.data;
};

// PUT
export const putData = async <T>(
  endpoint: string,
  payload: unknown
): Promise<T> => {
  const response = await axiosInstance.put<T>(endpoint, payload);
  return response.data;
};

// DELETE
export const deleteData = async <T>(endpoint: string): Promise<T> => {
  const response = await axiosInstance.delete<T>(endpoint);
  return response.data;
};

// PATCH 
export const patchData = async <T>(endpoint: string): Promise<T> => {
  const response = await axiosInstance.patch<T>(endpoint);
  return response.data;
};
