import { createContext, useContext } from "react";

export type User = {
  userId: string;
  userName: string;
  firstName: string;
  lastName: string;
  roles?: string[];
  unit?: {
    id: number;
    unitNumber: string;
    price: number;
    statusName: string | null;
  };
};

type GlobalContextType = {
  user: User | null;
  setUser: React.Dispatch<React.SetStateAction<User | null>>;
};

const GlobalContextObject: GlobalContextType = {
  user: null,
  setUser: () => {},
};

export const UserContext = createContext(GlobalContextObject);
const useGlobalContext = () => useContext(UserContext);

export default useGlobalContext;
