import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Booking } from '../_models/booking';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiUrl = environment.apiUrl + "/booking";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllBookings(): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.apiUrl);
  }

  getBooking(bookingId: number): Observable<Booking> {
    return this.http.get<Booking>(this.apiUrl + "/" + bookingId)
  }

  getBookingByShowingId(showingId: number): Observable<Booking[]> {
    return this.http.get<Booking[]>(this.apiUrl + "/GetByShowingId/" + showingId)
  }

  addBooking(booking: Booking): Observable<Booking> {
    return this.http.post<Booking>(this.apiUrl, booking, this.httpOptions)
  }

  deleteBooking(bookingId: number): Observable<Booking> {
    return this.http.delete<Booking>(this.apiUrl + '/' + bookingId, this.httpOptions)
  }

  updateBooking(bookingId: number, booking: Booking): Observable<Booking> {
    return this.http.put<Booking>(this.apiUrl + '/' + bookingId, booking, this.httpOptions)
  }
}
