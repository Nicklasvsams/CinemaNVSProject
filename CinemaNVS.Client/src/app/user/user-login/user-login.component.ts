import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Customer } from 'src/app/_models/customer';
import { Login } from 'src/app/_models/login';
import { CustomerService } from 'src/app/_services/customer.service';
import { LoginService } from 'src/app/_services/login.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  login: Login = { id: 0, username: '', password: '', isAdmin: false, customerId: 0 }
  customer: Customer = { id: 0, firstName: '', lastName: '', phoneNo: 0, email: '', isActive: true }
  loginPassword: string = '';

  constructor(private loginService: LoginService, private customerService: CustomerService, private route: Router) { }

  ngOnInit(): void {
  }

  create() {
    console.log(this.login);
    console.log(this.customer);
    this.customerService.addCustomer(this.customer)
      .subscribe((x) => {
        this.login.customerId = x.id;
        this.loginService.addLogin(this.login)
          .subscribe(() => {
            this.route.navigate(['']);
          });
      })
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
