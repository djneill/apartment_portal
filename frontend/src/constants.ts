import { Guest } from "./types";

export const ACTIVE_GUESTS: Guest[] = [
  {
    id: 16,
    firstName: "Jenn",
    lastName: "Dirt",
    phoneNumber: "5555555551",
    unitId: 1,
    checkInTime: "2024-03-01T10:00:00",
    checkOutTime: null,
    isApproved: false, // Active guest
  },
  {
    id: 17,
    firstName: "Jane",
    lastName: "Dirt",
    phoneNumber: "5555555552",
    unitId: 2,
    checkInTime: "2024-02-15T12:30:00",
    checkOutTime: "2024-02-20T15:00:00",
    isApproved: false, // Inactive guest
  },
  {
    id: 18,
    firstName: "Mike",
    lastName: "Stone",
    phoneNumber: "5555555553",
    unitId: 3,
    checkInTime: "2024-03-05T08:45:00",
    checkOutTime: null,
    isApproved: false, // Active guest
  },

];

export const INACTIVE_GUESTS: Guest[] = [
  {
    id: 19,
    firstName: "Lucy",
    lastName: "Smith",
    phoneNumber: "5555555554",
    unitId: 4,
    checkInTime: "2024-01-20T14:20:00",
    checkOutTime: "2024-01-25T09:00:00",
    isApproved: false, // Inactive guest
  },
  {
    id: 20,
    firstName: "Sam",
    lastName: "Johnson",
    phoneNumber: "5555555555",
    unitId: 5,
    checkInTime: "2024-03-10T11:15:00",
    checkOutTime: null,
    isApproved: false, // Active guest
  },
  {
    id: 21,
    firstName: "Emily",
    lastName: "Clark",
    phoneNumber: "5555555556",
    unitId: 6,
    checkInTime: "2024-02-05T16:45:00",
    checkOutTime: "2024-02-10T10:00:00",
    isApproved: false, // Inactive guest
  },

]
