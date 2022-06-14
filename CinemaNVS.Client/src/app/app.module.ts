import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FrontpageComponent } from './frontpage/frontpage.component';
import { MovieComponent } from './admin/movie/movie.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from './material/material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DirectorComponent } from './admin/director/director.component';
import { ActorComponent } from './admin/actor/actor.component';
import { MovieActorComponent } from './admin/movie-actor/movie-actor.component';
import { LoginComponent } from './admin/login/login.component';
import { CustomerComponent } from './admin/customer/customer.component';
import { ShowingComponent } from './admin/showing/showing.component';
import { BookingComponent } from './admin/booking/booking.component';
import { BookingSeatingComponent } from './admin/booking-seating/booking-seating.component';

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    MovieComponent,
    DirectorComponent,
    ActorComponent,
    MovieActorComponent,
    LoginComponent,
    CustomerComponent,
    ShowingComponent,
    BookingComponent,
    BookingSeatingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MaterialModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
