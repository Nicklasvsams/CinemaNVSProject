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

@NgModule({
  declarations: [
    AppComponent,
    FrontpageComponent,
    MovieComponent,
    DirectorComponent,
    ActorComponent,
    MovieActorComponent
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
