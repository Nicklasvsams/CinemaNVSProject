import { Component, OnInit } from '@angular/core';
import { Actor } from '../_models/actor';
import { Movie } from '../_models/movie';
import { Director } from '../_models/director';
import { MovieService } from '../_services/movie.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  movies: Movie[] = [];

  constructor(private movieService: MovieService, private route: Router) { }

  ngOnInit(): void {
    this.movieService.getAllMovies()
      .subscribe({
        next: (x) => {
          this.movies = x;
        },
        error: (err) => {
          console.log(err);
        }
      });
  }

  directToBookingPage(movie: Movie) {
    if (sessionStorage.getItem('user') == null) {
      alert('Please log in to book a showing!');
    }
    else {
      sessionStorage.setItem('movie', movie.id.toString());
      this.route.navigate(['user/booking']);
    }
  }
}
