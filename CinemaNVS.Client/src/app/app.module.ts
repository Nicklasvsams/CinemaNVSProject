import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

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
import { LoginAccountComponent } from './login-account/login-account.component';
import { JwtInterceptorService } from './_services/jwt-interceptor.service';
import { UnauthorizedComponent } from './admin/unauthorized/unauthorized.component';
import { UserAdminComponent } from './user/user-admin/user-admin.component';
import { UserBookingComponent } from './user/user-booking/user-booking.component';

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
    BookingSeatingComponent,
    LoginAccountComponent,
    UnauthorizedComponent,
    UserAdminComponent,
    UserBookingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MaterialModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
