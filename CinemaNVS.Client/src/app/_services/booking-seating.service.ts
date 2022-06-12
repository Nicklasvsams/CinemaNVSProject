import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { BookingSeating } from '../_models/bookingSeating';

@Injectable({
  providedIn: 'root'
})
export class BookingSeatingService {

  private apiUrl = environment.apiUrl + "/bookingseating";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllBookingSeatings(): Observable<BookingSeating[]> {
    return this.http.get<BookingSeating[]>(this.apiUrl);
  }

  addBookingSeating(bookingSeating: BookingSeating): Observable<BookingSeating> {
    return this.http.post<BookingSeating>(this.apiUrl, bookingSeating, this.httpOptions)
  }

  deleteBookingSeating(id: number): Observable<BookingSeating> {
    return this.http.delete<BookingSeating>(this.apiUrl + '/' + id, this.httpOptions)
  }
}
