import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActorComponent } from './admin/actor/actor.component';
import { CustomerComponent } from './admin/customer/customer.component';
import { DirectorComponent } from './admin/director/director.component';
import { LoginComponent } from './admin/login/login.component';
import { MovieActorComponent } from './admin/movie-actor/movie-actor.component';
import { MovieComponent } from './admin/movie/movie.component';
import { ShowingComponent } from './admin/showing/showing.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'admin/movie', component: MovieComponent },
  { path: 'admin/director', component: DirectorComponent },
  { path: 'admin/actor', component: ActorComponent },
  { path: 'admin/movie-actor', component: MovieActorComponent },
  { path: 'admin/login', component: LoginComponent },
  { path: 'admin/customer', component: CustomerComponent },
  { path: 'admin/showing', component: ShowingComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
