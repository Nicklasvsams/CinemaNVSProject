import { Booking } from "./booking";

export interface BookingSeating {
    id: number;
    bookingId: number;
    seatingId: number;
    bookingResponse?: Booking;
}