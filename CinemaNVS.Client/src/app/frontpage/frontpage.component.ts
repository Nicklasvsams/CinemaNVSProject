import { Component, OnInit } from '@angular/core';
import { Actor } from '../_models/actor';
import { Movie } from '../_models/movie';
import { Director } from '../_models/director';
import { MovieService } from '../_services/movie.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  movies: Movie[] = [];

  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    this.movieService.getAllMovies()
    .subscribe({
      next: (x) => {
        this.movies = x;
        console.log(x);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
