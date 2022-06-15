import { BookingSeating } from "./bookingSeating";
import { Customer } from "./customer";
import { Seating } from "./seating";
import { Showing } from "./showing";

export interface Booking {
    id: number;
    bookingDate: Date;
    showingId: number;
    customerId: number;
    customerResponse?: Customer;
    showingResponse?: Showing;
    seatingResponses?: Seating[];
}