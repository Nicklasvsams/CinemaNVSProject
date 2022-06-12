import { Component, OnInit } from '@angular/core';
import { isEmpty } from 'rxjs';
import { Login } from 'src/app/_models/login';
import { LoginService } from 'src/app/_services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  logins: Login[] = [];
  loginPassword: string = "";
  login: Login = { id: 0, username: "", password: "", isAdmin: false }

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
    this.loginService.getAllLogins()
      .subscribe(x => this.logins = x);
  }

  edit(login: Login): void {
    this.login = login;
  }

  delete(login: Login): void {
    if (confirm('Are you sure you want to delete this login?')) {
      this.loginService.deleteLogin(login.username)
        .subscribe(() => {
          this.logins = this.logins.filter(x => x.id != login.id)
        });
    }
  }

  save(): void {
    this.login.isAdmin = this.login.isAdmin.toString() === 'Yes' ? true : false;

    if (this.login.password === this.loginPassword && this.login.username !== "" && this.login.password !== "") {
      if (this.login.id == 0) {
        if (confirm('Save new login?')) {
          this.loginService.addLogin(this.login)
            .subscribe({
              next: (x) => {
                this.logins.push(x);
                this.login = this.loginObject();
              },
              error: (err) => {
                console.log(err);
              }
            });
        }
      }
      else {
        if (confirm('Update login with ID ' + this.login.id + '?')) {
          this.loginService.updateLogin(this.login.username, this.login)
            .subscribe({
              error: (err) => {
                console.log(err);
              }
            });

          console.log(this.login);
          this.login = this.loginObject();
        }
      }
    }
    else {
      let alertString = "";
      if (this.login.password !== this.loginPassword) alertString = alertString + "Passwords don't match. ";
      if (this.login.username === "") alertString = alertString + "Username can not be empty. "
      if (this.login.password === "") alertString = alertString + "Password can not be empty. "
      alert(alertString + "Please try again.");
    }
  }

  cancel(): void {
    this.login = this.loginObject();
    this.loginPassword = "";

    var passwordInputs = document.getElementsByClassName("passwords");
    for (let i = 0; i < passwordInputs.length; i++) {
      var passInputs = passwordInputs[i];
      if (passInputs.classList.contains('passwordNotValid')) {
        passInputs.classList.remove('passwordNotValid');
      }
      if (passInputs.classList.contains('passwordValid')) {
        passInputs.classList.remove('passwordValid');
      }
    }
  }

  loginObject(): Login {
    return { id: 0, username: "", password: "", isAdmin: false }
  }

  onChangeEvent(event: any) {
    if ((event.target as HTMLInputElement) != document.getElementById('passwordValid')) {
      var passwordInputs = document.getElementsByClassName("passwords");
      for (let i = 0; i < passwordInputs.length; i++) {
        var passInputs = passwordInputs[i];
        if (this.login.password === this.loginPassword) {
          if (passInputs.classList.contains('passwordNotValid')) {
            passInputs.classList.remove('passwordNotValid');
          }

          passInputs.classList.add('passwordValid');
        }
        else {
          if (passInputs.classList.contains('passwordValid')) {
            passInputs.classList.remove('passwordValid');
          }

          passInputs.classList.add('passwordNotValid');
        }
      }
    }

  }
}
