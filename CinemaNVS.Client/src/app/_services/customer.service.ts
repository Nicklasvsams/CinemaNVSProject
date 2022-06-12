import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Customer } from '../_models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private apiUrl = environment.apiUrl + "/customer";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  constructor(private http: HttpClient) { }

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiUrl);
  }

  getCustomer(customerId: number): Observable<Customer> {
    return this.http.get<Customer>(this.apiUrl + "/" + customerId)
  }

  addCustomer(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl, customer, this.httpOptions)
  }

  deleteCustomer(customerId: number): Observable<Customer> {
    return this.http.delete<Customer>(this.apiUrl + '/' + customerId, this.httpOptions)
  }

  updateCustomer(customerId: number, customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(this.apiUrl + '/' + customerId, customer, this.httpOptions)
  }
}
