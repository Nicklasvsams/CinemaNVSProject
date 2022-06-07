import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Movie } from '../_models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private apiUrl = environment.apiUrl + "/movie";

  constructor(private http:HttpClient) { }

  getAllMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.apiUrl);
  }
}
