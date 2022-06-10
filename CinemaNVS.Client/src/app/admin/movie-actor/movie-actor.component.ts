import { Component, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { Movie } from 'src/app/_models/movie';
import { MovieActor } from 'src/app/_models/movieActor';
import { ActorService } from 'src/app/_services/actor.service';
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
  actors: Actor[] = [];

  constructor(private movieActorService: MovieActorService, private movieService: MovieService, private actorService: ActorService) { }

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

    this.actorService.getAllActors()
      .subscribe({
        next: (x) => {
          this.actors = x;
        }
      })

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
            this.movieService.getMovie(x.movieId)
              .subscribe({
                next: (y) => {
                  x.movie = y;
                  this.movieActors.push(x);
                  this.movieActor = this.movieActorObject();
                }
              });
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
