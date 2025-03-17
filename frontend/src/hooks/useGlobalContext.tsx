import { createContext, useContext } from "react";

export type User = {
  userId: string;
  userName: string;
  firstName: string;
  lastName: string;
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
