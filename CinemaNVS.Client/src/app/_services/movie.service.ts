import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../_models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private apiUrl = environment.apiUrl + "/movie";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http:HttpClient) { }

  getAllMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.apiUrl);
  }

  getMovie(movieId: number): Observable<Movie> {
    return this.http.get<Movie>(this.apiUrl + "/" + movieId)
  }

  addMovie(movie: Movie): Observable<Movie> {
    return this.http.post<Movie>(this.apiUrl, movie, this.httpOptions)
  }

  deleteMovie(movieId: number): Observable<Movie> {
    return this.http.delete<Movie>(this.apiUrl + '/' + movieId, this.httpOptions)
  }

  updateMovie(movieId: number, movie: Movie): Observable<Movie> {
    return this.http.put<Movie>(this.apiUrl + '/' + movieId, movie, this.httpOptions)
  }
}
