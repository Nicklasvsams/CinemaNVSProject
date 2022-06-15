import { Component, OnInit } from '@angular/core';
import { Booking } from 'src/app/_models/booking';
import { BookingSeating } from 'src/app/_models/bookingSeating';
import { Seating } from 'src/app/_models/seating';
import { BookingSeatingService } from 'src/app/_services/booking-seating.service';
import { BookingService } from 'src/app/_services/booking.service';
import { CustomerService } from 'src/app/_services/customer.service';
import { SeatingService } from 'src/app/_services/seating.service';
import { ShowingService } from 'src/app/_services/showing.service';

@Component({
  selector: 'app-booking-seating',
  templateUrl: './booking-seating.component.html',
  styleUrls: ['./booking-seating.component.css']
})
export class BookingSeatingComponent implements OnInit {
  authorization: any = sessionStorage?.getItem('role');

  bookingSeatings: BookingSeating[] = [];
  bookingSeating: BookingSeating = { id: 0, bookingId: 0, seatingId: 0 };
  bookings: Booking[] = [];
  seatings: Seating[] = [];

  constructor(private bookingSeatingService: BookingSeatingService, private bookingService: BookingService, private seatingService: SeatingService, private showingService: ShowingService, private customerService: CustomerService) { }

  ngOnInit(): void {
    this.bookingService.getAllBookings()
      .subscribe({
        next: (x) => {
          this.bookings = x;

          this.bookingSeatingService.getAllBookingSeatings()
            .subscribe({
              next: (x) => {
                this.bookingSeatings = x;
                this.bookingSeatings.forEach((bookingSeating, index) =>
                  this.bookingSeatings[index].bookingResponse = this.bookings
                    .find(x => x.id == bookingSeating.bookingId));
              }
            });
        }
      }
      );

    this.seatingService.getAllSeatings()
      .subscribe({
        next: (x) => {
          this.seatings = x;
        }
      });
  }

  delete(bookingSeating: BookingSeating): void {
    if (confirm('Are you sure you want to delete this booking seating relation?')) {

      this.bookingSeatingService.deleteBookingSeating(bookingSeating.id)
        .subscribe(() => {
          this.bookingSeatings = this.bookingSeatings.filter(x => x.id != bookingSeating.id)
        });
    }
  }

  save(): void {
    if (confirm('Save new booking seating relation?')) {

      this.bookingSeatingService.addBookingSeating(this.bookingSeating)
        .subscribe({
          next: (x) => {
            this.bookingService.getBooking(x.bookingId)
              .subscribe({
                next: (y) => {
                  x.bookingResponse = y;
                  this.bookingSeatings.push(x);
                  this.bookingSeating = this.bookingSeatingObject();
                }
              });
          },
          error: (err) => {
            console.log(err);
          }
        });
    }
  }

  cancel(): void {
    this.bookingSeating = this.bookingSeatingObject();
    console.log(this.bookingSeatings);
  }

  bookingSeatingObject(): BookingSeating {
    return { id: 0, bookingId: 0, seatingId: 0 }
  }
}
