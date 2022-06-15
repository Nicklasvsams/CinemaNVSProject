import { Booking } from "./booking";

export interface Customer {
    id: number;
    firstName: string;
    lastName: string;
    phoneNo: number;
    email: string;
    isActive: boolean;
    bookingResponses?: Booking[];
}