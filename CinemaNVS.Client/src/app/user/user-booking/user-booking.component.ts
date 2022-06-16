import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Booking } from 'src/app/_models/booking';
import { BookingSeating } from 'src/app/_models/bookingSeating';
import { Movie } from 'src/app/_models/movie';
import { BookingSeatingService } from 'src/app/_services/booking-seating.service';
import { BookingService } from 'src/app/_services/booking.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-user-booking',
  templateUrl: './user-booking.component.html',
  styleUrls: ['./user-booking.component.css']
})
export class UserBookingComponent implements OnInit {

  movie: Movie = { id: 0, title: '', runtimeMinutes: 0, releaseDate: new Date(), isRunning: false, trailerLink: '', imdbLink: '', directorId: 0 }
  bookingsByShowing: Booking[] = [];
  seats: any = null;
  selectedSeats: number = 0;

  showingId: number = 0;
  movieId: any = sessionStorage.getItem('movie');
  customerId: any = sessionStorage.getItem('cusId');

  cinemaContainer: any = document.querySelector('.cinemaContainer');

  constructor(private movieService: MovieService, private bookingService: BookingService, private bookingSeatingService: BookingSeatingService, private route: Router) { }

  ngOnInit(): void {
    if (sessionStorage.getItem('movie') != null) {
      this.movieService.getMovie(parseInt(this.movieId))
        .subscribe({
          next: (x) => {
            this.movie = x;
          }
        })
    }
  }

  onChangeEvent(event: any) {
    this.seats = document.getElementsByClassName('seat');

    for (let i = 0; i < this.seats.length; i++) {
      this.seats[i].classList.remove('occupied');
      this.seats[i].classList.remove('selected');
    }

    this.bookingService.getBookingByShowingId(this.showingId)
      .subscribe({
        next: (x) => {
          this.bookingsByShowing = x;
          this.bookingsByShowing.forEach((x, index) =>
            this.bookingsByShowing[index].seatingResponses?.forEach((seating) => {
              this.seats[seating.id - 1].classList.add('occupied')
            })
          );
        }
      });
  }

  onClickEvent(event: any) {
    if (event.target.classList.contains('seat') && !event.target.classList.contains('occupied')) {
      event.target.classList.toggle('selected');
    }

    if (event.target.classList.contains('selected')) {
      this.selectedSeats = this.selectedSeats + 1;
    }
    else {
      this.selectedSeats = this.selectedSeats - 1;
    }
  }

  bookShowing(): void {
    if (this.showingId == 0) {
      alert('Please choose a showing to book')
    }
    else {
      let booking: Booking = { id: 0, bookingDate: new Date(), customerId: this.customerId, showingId: this.showingId }
      this.bookingService.addBooking(booking)
        .subscribe({
          next: (x) => {
            for (let index = 0; index < this.seats.length; index++) {
              if (this.seats[index].classList.contains('selected')) {
                let bookingSeating: BookingSeating = { id: 0, bookingId: x.id, seatingId: index + 1 }
                this.bookingSeatingService.addBookingSeating(bookingSeating)
                  .subscribe(() => {
                    this.route.navigate(['']);
                  });
              }
            }
          }
        })
    }
  }
}
