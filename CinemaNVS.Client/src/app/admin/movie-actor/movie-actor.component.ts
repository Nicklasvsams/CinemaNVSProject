import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { MovieActor } from 'src/app/_models/movieActor';
import { MovieActorService } from 'src/app/_services/movie-actor.service';
import { MovieService } from 'src/app/_services/movie.service';

@Component({
  selector: 'app-movie-actor',
  templateUrl: './movie-actor.component.html',
  styleUrls: ['./movie-actor.component.css']
})

export class MovieActorComponent implements OnInit {
  movieActors: MovieActor[] = [];
  movieActor: MovieActor = { id: 0, movieId: 0, actorId: 0 };
  movies: Movie[] = [];

  constructor(private movieActorService: MovieActorService, private movieService: MovieService) { }

  ngOnInit(): void {
    this.movieService.getAllMovies()
      .subscribe({
        next: (x) => {
          this.movies = x;
          this.movieActorService.getAllMovieActors()
            .subscribe({
              next: (x) => {
                this.movieActors = x;
                this.movieActors.forEach((movieActor, index) =>
                  this.movieActors[index].movie = this.movies
                    .find(x => x.id == movieActor.movieId));
              }
            });
        }
      });


  }

  delete(movieActor: MovieActor): void {
    if (confirm('Are you sure you want to delete this movie actor relation?')) {

      this.movieActorService.deleteMovieActor(movieActor.id)
        .subscribe(() => {
          this.movieActors = this.movieActors.filter(x => x.id != movieActor.id)
        });
    }
  }

  save(): void {
    if (confirm('Save new movie actor relation?')) {

      this.movieActorService.addMovieActor(this.movieActor)
        .subscribe({
          next: (x) => {
            this.ngOnInit();
            x.movie = this.movies.find(y => y.id == x.movieId);
            this.movieActors.push(x);
            this.movieActor = this.movieActorObject();
          },
          error: (err) => {
            console.log(err);
          }
        });
    }
  }

  cancel(): void {
    this.movieActor = this.movieActorObject();
    console.log(this.movieActors);
  }

  movieActorObject(): MovieActor {
    return { id: 0, movieId: 0, actorId: 0 }
  }
}
