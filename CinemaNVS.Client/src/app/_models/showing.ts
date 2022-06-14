import { Time } from "@angular/common";
import { Booking } from "./booking";
import { Movie } from "./movie";

export interface Showing {
    id: number;
    price: number;
    timeOfShowing: Date;
    movieId: number;
    date?: Date;
    time?: Date;
    movieResponse?: Movie;
    bookingResponses?: Booking[];
}