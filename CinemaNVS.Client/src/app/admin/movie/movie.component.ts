import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { Director } from 'src/app/_models/director';
import { MovieService } from 'src/app/_services/movie.service';
import { DirectorService } from 'src/app/_services/director.service';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit {
  movies: Movie[] = [];
  directors: Director[] = [];
  movie: Movie = { id: 0, title: '', rating: 0, runtimeMinutes: 0, releaseDate: new Date(), isRunning: false, trailerLink: '', imdbLink: '', directorId: 0 };

  constructor(private movieService: MovieService, private directorService: DirectorService) { }

  ngOnInit(): void {
    this.movieService.getAllMovies()
      .subscribe(x => this.movies = x);

    this.directorService.getAllDirectors()
      .subscribe(x => this.directors = x);
  }

  edit(movie: Movie): void {
    this.movie = movie;
  }

  delete(movie: Movie): void {
    if (confirm('Are you sure you want to delete this movie?')) {
      this.movieService.deleteMovie(movie.id)
        .subscribe(() => {
          this.movies = this.movies.filter(x => x.id != movie.id)
        });
    }
  }

  save(): void {
    this.movie.isRunning = this.movie.isRunning.toString() === 'Yes' ? true : false;

    if (this.movie.id == 0) {
      if (confirm('Save new movie?')) {

        this.movieService.addMovie(this.movie)
          .subscribe({
            next: (x) => {
              this.movies.push(x);
              console.log(this.movies)
              this.movie = this.movieObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update movie with ID ' + this.movie.id + '?')) {
        this.movieService.updateMovie(this.movie.id, this.movie)
          .subscribe({
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.movie = this.movieObject();
  }

  movieObject(): Movie {
    return { id: 0, title: '', rating: 0, runtimeMinutes: 0, releaseDate: new Date(), isRunning: false, trailerLink: '', imdbLink: '', directorId: 0 }
  }
}
