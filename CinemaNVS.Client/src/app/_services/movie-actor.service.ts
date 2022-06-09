import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MovieActor } from '../_models/movieActor';

@Injectable({
  providedIn: 'root'
})
export class MovieActorService {

  private apiUrl = environment.apiUrl + "/movieactor";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllMovieActors(): Observable<MovieActor[]> {
    return this.http.get<MovieActor[]>(this.apiUrl);
  }

  addMovieActor(movieActor: MovieActor): Observable<MovieActor> {
    return this.http.post<MovieActor>(this.apiUrl, movieActor, this.httpOptions)
  }

  deleteMovieActor(id: number): Observable<MovieActor> {
    return this.http.delete<MovieActor>(this.apiUrl + '/' + id, this.httpOptions)
  }
}
