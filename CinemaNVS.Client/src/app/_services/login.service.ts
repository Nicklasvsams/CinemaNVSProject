import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from '../_models/login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  private apiUrl = environment.apiUrl + "/login";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllLogins(): Observable<Login[]> {
    return this.http.get<Login[]>(this.apiUrl);
  }

  getLogin(username: string): Observable<Login> {
    return this.http.get<Login>(this.apiUrl + "/" + username)
  }

  authorizeLogin(username: string, password: string): Observable<Login> {
    return this.http.get<Login>(this.apiUrl + "/" + username + ", " + password)
  }

  addLogin(login: Login): Observable<Login> {
    return this.http.post<Login>(this.apiUrl, login, this.httpOptions)
  }

  deleteLogin(loginId: number): Observable<Login> {
    return this.http.delete<Login>(this.apiUrl + '/' + loginId, this.httpOptions)
  }

  updateLogin(loginId: number, director: Login): Observable<Login> {
    return this.http.put<Login>(this.apiUrl + '/' + loginId, director, this.httpOptions)
  }
}
