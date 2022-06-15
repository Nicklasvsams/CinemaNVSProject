import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/_models/booking';
import { Customer } from 'src/app/_models/customer';
import { Seating } from 'src/app/_models/seating';
import { Showing } from 'src/app/_models/showing';
import { BookingService } from 'src/app/_services/booking.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { SeatingService } from 'src/app/_services/seating.service';
import { ShowingService } from 'src/app/_services/showing.service';

@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  bookings: Booking[] = [];
  customers: Customer[] = [];
  showings: Showing[] = [];
  seatings: Seating[] = [];
  bookingInputs: HTMLCollection = document.getElementsByClassName('updateBookingInput');
  booking: Booking = { id: 0, bookingDate: new Date(), showingId: 0, customerId: 0 }

  constructor(private bookingService: BookingService, private customerService: CustomerService, private showingService: ShowingService, private seatingService: SeatingService) { }

  ngOnInit(): void {
    this.bookingService.getAllBookings()
      .subscribe(x => this.bookings = x);

    this.customerService.getAllCustomers()
      .subscribe(x => this.customers = x);

    this.showingService.getAllShowings()
      .subscribe(x => this.showings = x);

    this.seatingService.getAllSeatings()
      .subscribe(x => this.seatings = x);
  }

  edit(booking: Booking): void {
    this.booking = booking;

    for (let i = 0; i < this.bookingInputs.length; i++) {
      var booInput = this.bookingInputs[i];
      booInput.classList.add('hideUpdateBookingInput')
    }
  }

  delete(booking: Booking): void {
    if (confirm('Are you sure you want to delete this booking?')) {
      this.bookingService.deleteBooking(booking.id)
        .subscribe(() => {
          this.bookings = this.bookings.filter(x => x.id != booking.id)
        });
    }
  }

  save(): void {
    console.log(this.booking);

    if (this.booking.id == 0) {
      if (confirm('Save new booking?')) {
        this.bookingService.addBooking(this.booking)
          .subscribe({
            next: (x) => {
              this.bookings.push(x);
              this.booking = this.bookingObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update booking with ID ' + this.booking.id + '?')) {
        this.bookingService.updateBooking(this.booking.id, this.booking)
          .subscribe({
            next: (x) => {
              x.seatingResponses = this.bookings.find(y => y.id == x.id)?.seatingResponses;
              this.bookings = this.bookings.filter(y => y.id != x.id);
              x.customerResponse = this.customers.find(y => y.id == x.customerId);
              x.showingResponse = this.showings.find(y => y.id == x.showingId);
              this.bookings.push(x);
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.booking = this.bookingObject();
    for (let i = 0; i < this.bookingInputs.length; i++) {
      var booInput = this.bookingInputs[i];
      booInput.classList.remove('hideUpdateBookingInput')
    }
  }

  bookingObject(): Booking {
    return { id: 0, bookingDate: new Date(), showingId: 0, customerId: 0 }
  }
}
