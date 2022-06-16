import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/_models/booking';
import { Login } from 'src/app/_models/login';
import { BookingService } from 'src/app/_services/booking.service';
import { LoginService } from 'src/app/_services/login.service';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.css']
})
export class UserAdminComponent implements OnInit {
  login: Login = { id: 0, username: "", password: "", isAdmin: false, customerId: 0 }

  constructor(private loginService: LoginService, private bookingService: BookingService) { }

  ngOnInit(): void {
    let sessionUser: any = sessionStorage.getItem('user');

    if (sessionUser != null) {
      this.loginService.getLogin(sessionUser)
        .subscribe({
          next: (x) => {
            this.login = x;
            console.log(x);
          }
        });
    }
  }

  delete(booking: Booking): void {
    if (confirm('Are you sure you want to delete this booking?')) {
      this.bookingService.deleteBooking(booking.id)
        .subscribe(() => {
          if (this.login.customerResponse != null && this.login.customerResponse.bookingResponses != null) {
            this.login.customerResponse.bookingResponses = this.login.customerResponse.bookingResponses.filter(x => x.id != booking.id)
          }
        });
    }
  }

}
