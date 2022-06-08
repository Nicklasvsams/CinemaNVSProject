import { Component, OnInit } from '@angular/core';
import { Director } from 'src/app/_models/director';
import { DirectorService } from 'src/app/_services/director.service';

@Component({
  selector: 'app-director',
  templateUrl: './director.component.html',
  styleUrls: ['./director.component.css']
})
export class DirectorComponent implements OnInit {

  directors: Director[] = [];
  director: Director = { id: 0, name: '', imdbLink: '' }

  constructor(private directorService: DirectorService) { }

  ngOnInit(): void {
    this.directorService.getAllDirectors()
      .subscribe(x => this.directors = x);
  }

  edit(director: Director): void {
    this.director = director;
  }

  delete(director: Director): void {
    if (confirm('Are you sure you want to delete this director?')) {
      this.directorService.deleteDirector(director.id)
        .subscribe(() => {
          this.directors = this.directors.filter(x => x.id != director.id)
        });
    }
  }

  save(): void {
    if (this.director.id == 0) {
      if (confirm('Save new director?')) {
        this.directorService.addDirector(this.director)
          .subscribe({
            next: (x) => {
              this.directors.push(x);
              this.director = this.directorObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update director with ID ' + this.director.id + '?')) {
        this.directorService.updateDirector(this.director.id, this.director)
          .subscribe({
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.director = this.directorObject();
  }

  directorObject(): Director {
    return { id: 0, name: '', imdbLink: '' }
  }
}
