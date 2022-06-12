import { Actor } from "./actor";
import { Movie } from "./movie";

export interface MovieActor {
    id: number;
    movieId: number;
    actorId: number;
    movie?: Movie;
}