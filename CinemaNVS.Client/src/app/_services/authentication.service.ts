import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthorizedLogin } from '../_models/authorizedLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private apiUrl = environment.apiUrl + '/Login/';

  constructor(private http: HttpClient) {

  }

  AuthenticateLogin(authorizedLogin: AuthorizedLogin): Observable<AuthorizedLogin> {
    return this.http.get<any>(this.apiUrl + authorizedLogin.loginResponse.username + '/' + authorizedLogin.loginResponse.password);
  }
}
