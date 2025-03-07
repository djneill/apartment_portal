import axios from 'axios';

const BASE_URL = import.meta.env.VITE_API;

export const login = async (email: string, password: string): Promise<{ token: string }> => {
    try {
      const response = await axios.post<{ token: string }>(`${BASE_URL}login`, {
        email,
        password,
      }, {
        headers: {
          'accept': 'application/json',
          'Content-Type': 'application/json',
        },      withCredentials: true,
      });
      return response.data;
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    }
  };