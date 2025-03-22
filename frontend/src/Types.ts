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
export type ApiIssue ={
  id: number;
  description: string;
  createdOn: string;
  issueType: {
    id: number;
    name: string;
  };
  status: {
    id: number;
    name: string;
  };
  user: {
    id: number;
    firstName: string;
    lastName: string;
    dateOfBirth: string;
    status: {
      id: number;
      name: string;
    };
  };
}
export interface Issue {
  id: number;
  date: string;
  title: string;
  isNew: boolean;
  disabled: boolean;
  type: string;
}