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

export interface Guest {
  id: number;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  userId: number;
  createdOn: string;
  expiration: string;
  accessCode: number;
}

export interface GuestsResponse {
  message?: string;
  data: Guest[];
}

export interface GuestRequest {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  accessCode: string;
  durationInHours: number;
  carMake?: string;
  carModel?: string;
  carColor?: string;
  licensePlate?: string;
}

export interface TimerProps {
  expiration: string;
  className: string;
}

export type ApiIssue = {
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
};

export interface Issue {
  id: number;
  date: string;
  title: string;
  isNew: boolean;
  disabled: boolean;
  type: string;
  status: string;
  created: string;
  user: string;
}

export interface PackagesResponse {
  message?: string;
  data: Packages[];
}

export interface Packages {
  id: number;
  lockerNumber: number;
  code: number;
  status: {
    id: number;
    name: string;
  };
}

export interface Lease {
  id: number;
  startDate: string,
  endDate: string,
  signedOn: string,
  link: string,
  unitUser: {
    userId: number,
    unitId: number,
  },
  status: {
    id: number,
    name: string
  }
}

export type ToastType = "success" | "error" | "info" | "warning";

export interface ToastOptions {
  type?: ToastType;
  duration?: number;
}

export interface Toast {
  id: number;
  message: string;
  type: ToastType;
  duration: number;
}

