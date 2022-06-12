import { Booking } from "./booking";
import { Movie } from "./movie";

export interface Showing {
    id: number;
    price: number;
    timeOfShowing: Date;
    movieId: number;
    movieResponse?: Movie;
    bookingResponses?: Booking[];
}