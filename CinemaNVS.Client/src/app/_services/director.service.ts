import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Director } from '../_models/director';

@Injectable({
  providedIn: 'root'
})
export class DirectorService {

  private apiUrl = environment.apiUrl + "/director";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllDirectors(): Observable<Director[]> {
    return this.http.get<Director[]>(this.apiUrl);
  }

  getDirector(directorId: number): Observable<Director> {
    return this.http.get<Director>(this.apiUrl + "/" + directorId)
  }

  addDirector(director: Director): Observable<Director> {
    return this.http.post<Director>(this.apiUrl, director, this.httpOptions)
  }

  deleteDirector(directorId: number): Observable<Director> {
    return this.http.delete<Director>(this.apiUrl + '/' + directorId, this.httpOptions)
  }

  updateDirector(directorId: number, director: Director): Observable<Director> {
    return this.http.put<Director>(this.apiUrl + '/' + directorId, director, this.httpOptions)
  }
}
