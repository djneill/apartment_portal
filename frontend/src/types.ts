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
