import { Booking } from "./booking";
import { Login } from "./login";

export interface Customer {
    id: number;
    firstName: string;
    lastName: string;
    phoneNo: number;
    email: string;
    isActive: boolean;
    loginId: number;
    loginResponse?: Login;
    bookingResponses?: Booking[];
}