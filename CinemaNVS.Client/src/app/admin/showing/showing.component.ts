import { Component, OnInit } from '@angular/core';
import { Movie } from 'src/app/_models/movie';
import { Showing } from 'src/app/_models/showing';
import { MovieService } from 'src/app/_services/movie.service';
import { ShowingService } from 'src/app/_services/showing.service';

@Component({
  selector: 'app-showing',
  templateUrl: './showing.component.html',
  styleUrls: ['./showing.component.css']
})
export class ShowingComponent implements OnInit {

  showings: Showing[] = [];
  movies: Movie[] = [];
  showing: Showing = { id: 0, price: 0, timeOfShowing: new Date(), movieId: 0 }

  constructor(private showingService: ShowingService, private movieService: MovieService) { }

  ngOnInit(): void {
    this.showingService.getAllShowings()
      .subscribe(x => this.showings = x);

    this.movieService.getAllMovies()
      .subscribe(x => this.movies = x);
  }

  edit(showing: Showing): void {
    this.showing = showing;
  }

  delete(showing: Showing): void {
    if (confirm('Are you sure you want to delete this showing?')) {
      this.showingService.deleteShowing(showing.id)
        .subscribe(() => {
          this.showings = this.showings.filter(x => x.id != showing.id)
        });
    }
  }

  save(): void {
    if (this.showing.id == 0) {
      if (confirm('Save new showing?')) {
        this.showingService.addShowing(this.showing)
          .subscribe({
            next: (x) => {
              this.showings.push(x);
              this.showing = this.showingObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update showing with ID ' + this.showing.id + '?')) {
        this.showingService.updateShowing(this.showing.id, this.showing)
          .subscribe({
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.showing = this.showingObject();
  }

  showingObject(): Showing {
    return { id: 0, price: 0, timeOfShowing: new Date(), movieId: 0 }
  }
}
