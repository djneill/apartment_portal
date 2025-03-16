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
  data?: Guest[];
}

export interface GuestRequest {
  firstName: string;
  lastName: string;
  phoneNumber: string;
  accessCode: string;
  expiration: string;
  carMake?: string;
  carModel?: string;
  carColor?: string;
  licensePlate?: string;
}

export interface TimerProps {
  expiration: string;
  className: string;
}
