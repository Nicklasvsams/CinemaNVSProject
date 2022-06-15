import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/_models/customer';
import { Login } from 'src/app/_models/login';
import { CustomerService } from 'src/app/_services/customer.service';
import { LoginService } from 'src/app/_services/login.service';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.css']
})
export class UserAdminComponent implements OnInit {
  customer: Customer = { id: 0, firstName: '', lastName: '', phoneNo: 0, email: '', isActive: false }
  login: Login = { id: 0, username: "", password: "", isAdmin: false, customerId: 0 }

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

}
