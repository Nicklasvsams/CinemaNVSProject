import { Component, OnInit } from '@angular/core';
import { Customer } from 'src/app/_models/customer';
import { Login } from 'src/app/_models/login';
import { CustomerService } from 'src/app/_services/customer.service';
import { LoginService } from 'src/app/_services/login.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  customers: Customer[] = [];
  logins: Login[] = [];
  customer: Customer = { id: 0, firstName: "", lastName: "", phoneNo: 0, email: "", isActive: false, loginId: 0 };

  constructor(private customerService: CustomerService, private loginService: LoginService) { }

  ngOnInit(): void {
    this.customerService.getAllCustomers()
      .subscribe(x => this.customers = x);

    this.loginService.getAllLogins()
      .subscribe(x => this.logins = x);
  }

  edit(customer: Customer): void {
    this.customer = customer;
  }

  delete(customer: Customer): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(customer.id)
        .subscribe(() => {
          this.customers = this.customers.filter(x => x.id != customer.id)
        });
    }
  }

  save(): void {
    this.customer.isActive = this.customer.isActive.toString() === 'Yes' ? true : false;

    if (this.customer.id == 0) {
      if (confirm('Save new customer?')) {
        this.customerService.addCustomer(this.customer)
          .subscribe({
            next: (x) => {
              this.customers.push(x);
              this.customer = this.customerObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update customer with ID ' + this.customer.id + '?')) {
        this.customerService.updateCustomer(this.customer.id, this.customer)
          .subscribe({
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.customer = this.customerObject();
  }

  customerObject(): Customer {
    return { id: 0, firstName: "", lastName: "", phoneNo: 0, email: "", isActive: false, loginId: 0 };
  }
}
