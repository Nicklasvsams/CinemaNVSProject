import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizedLogin } from '../_models/authorizedLogin';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-login-account',
  templateUrl: './login-account.component.html',
  styleUrls: ['./login-account.component.css']
})
export class LoginAccountComponent implements OnInit {

  authLogin: AuthorizedLogin = { loginResponse: { id: 0, username: '', password: '', isAdmin: false }, jwToken: "" }

  @Output() sessionInfoEvent = new EventEmitter<void>();

  constructor(private authenticationService: AuthenticationService, private route: Router) { }

  ngOnInit(): void {
  }

  loginAccount(): void {
    this.authenticationService.AuthenticateLogin(this.authLogin)
      .subscribe(x => {
        if (x.loginResponse.isAuthorized == true) {
          console.log(x);
          this.authLogin = x;
          sessionStorage.setItem('token', this.authLogin.jwToken);
          sessionStorage.setItem('user', this.authLogin.loginResponse.username);
          sessionStorage.setItem('role', this.authLogin.loginResponse.isAdmin ? "admin" : "user");
          this.sessionInfoEvent.emit();
          this.route.navigate(['']);
        }
      });
  }
}
