import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Actor } from '../_models/actor';

@Injectable({
  providedIn: 'root'
})
export class ActorService {

  private apiUrl = environment.apiUrl + "/actor";

  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }
  constructor(private http: HttpClient) { }

  getAllActors(): Observable<Actor[]> {
    return this.http.get<Actor[]>(this.apiUrl);
  }

  getActor(actorId: number): Observable<Actor> {
    return this.http.get<Actor>(this.apiUrl + "/" + actorId)
  }

  addActor(actor: Actor): Observable<Actor> {
    return this.http.post<Actor>(this.apiUrl, actor, this.httpOptions)
  }

  deleteActor(actorId: number): Observable<Actor> {
    return this.http.delete<Actor>(this.apiUrl + '/' + actorId, this.httpOptions)
  }

  updateActor(actorId: number, actor: Actor): Observable<Actor> {
    return this.http.put<Actor>(this.apiUrl + '/' + actorId, actor, this.httpOptions)
  }
}
