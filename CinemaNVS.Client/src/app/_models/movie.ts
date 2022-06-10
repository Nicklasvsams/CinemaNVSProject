import { Actor } from "./actor";
import { Director } from "./director";

export interface Movie {
    id: number;
    title: string;
    runtimeMinutes: number;
    releaseDate: Date;
    isRunning: boolean;
    trailerLink: string;
    imdbLink: string;
    directorId: number;
    directorResponse?: Director;
    actorResponse?: Actor[];
}