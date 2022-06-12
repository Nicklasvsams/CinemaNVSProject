import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Seating } from '../_models/seating';

@Injectable({
  providedIn: 'root'
})
export class SeatingService {

  private apiUrl = environment.apiUrl + "/seating";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllSeatings(): Observable<Seating[]> {
    return this.http.get<Seating[]>(this.apiUrl);
  }
}
