import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Showing } from '../_models/showing';

@Injectable({
  providedIn: 'root'
})
export class ShowingService {

  private apiUrl = environment.apiUrl + "/showing";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllShowings(): Observable<Showing[]> {
    return this.http.get<Showing[]>(this.apiUrl);
  }

  getShowing(showingId: number): Observable<Showing> {
    return this.http.get<Showing>(this.apiUrl + "/" + showingId)
  }

  addShowing(showing: Showing): Observable<Showing> {
    return this.http.post<Showing>(this.apiUrl, showing, this.httpOptions)
  }

  deleteShowing(showingId: number): Observable<Showing> {
    return this.http.delete<Showing>(this.apiUrl + '/' + showingId, this.httpOptions)
  }

  updateShowing(showingId: number, showing: Showing): Observable<Showing> {
    return this.http.put<Showing>(this.apiUrl + '/' + showingId, showing, this.httpOptions)
  }
}
