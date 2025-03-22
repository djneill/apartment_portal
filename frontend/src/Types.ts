export type CurrentUserResponseType = {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
};

export type User = {
  userId: number;
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
