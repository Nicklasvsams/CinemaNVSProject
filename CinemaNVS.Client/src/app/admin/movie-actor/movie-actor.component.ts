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
  movieActor: MovieActor = { movieId: 0, actorId: 0 };
  movies: Movie[] = [];
  actors: Actor[] = [];

  constructor(private movieActorService: MovieActorService, private movieService: MovieService, private actorService: ActorService) { }

  ngOnInit(): void {
    this.movieService.getAllMovies()
      .subscribe(x => this.movies = x);

    this.actorService.getAllActors()
      .subscribe(x => this.actors = x);

    this.movieActorService.getAllMovieActors()
      .subscribe(x => this.movieActors = x);
  }

  edit(movieActor: MovieActor): void {
    this.movieActor = movieActor;
  }

  delete(movieActor: MovieActor): void {
    if (confirm('Are you sure you want to delete this movie actor relation?')) {
      this.movieActorService.deleteMovieActor(movieActor.id)
        .subscribe(() => {
          this.movies = this.movies.filter(x => x.id != movieActor.id)
        });
    }
  }

  save(): void {
    if (confirm('Save new movie actor relation?')) {

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

  cancel(): void {
    this.movieActor = this.movieActorObject();
  }

  movieActorObject(): MovieActor {
    return { movieId: 0, actorId: 0 }
  }
}
