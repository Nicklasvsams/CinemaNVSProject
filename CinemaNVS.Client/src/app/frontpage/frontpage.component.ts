import { Component, OnInit } from '@angular/core';
import { Movie } from '../_models/movie';
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
    // this.movies.push({ id: 1, title: "Test", rating: 5, runtimeMinutes: 69, releaseDate: new Date('1995-12-17T03:24:00').toLocaleDateString(), isRunning: true, trailerLink: "test.dk", imdbLink: "test2.dk", directorId: 1 });
    // this.movies.push({ id: 2, title: "Test2", rating: 10, runtimeMinutes: 420, releaseDate: new Date('1991-06-28T00:00:00').toLocaleDateString(), isRunning: false, trailerLink: "test2.dk", imdbLink: "test.dk", directorId: 2 });
  
    this.movieService.getAllMovies()
      .subscribe(x => this.movies = x);
  }

}
