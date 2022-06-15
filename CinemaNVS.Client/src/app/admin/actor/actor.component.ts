import { Component, OnInit } from '@angular/core';
import { Actor } from 'src/app/_models/actor';
import { ActorService } from 'src/app/_services/actor.service';

@Component({
  selector: 'app-actor',
  templateUrl: './actor.component.html',
  styleUrls: ['./actor.component.css']
})
export class ActorComponent implements OnInit {

  actors: Actor[] = [];
  actor: Actor = { id: 0, name: '', imdbLink: '' }

  constructor(private actorService: ActorService) { }

  ngOnInit(): void {
    this.actorService.getAllActors()
      .subscribe(x => this.actors = x);
  }

  edit(actor: Actor): void {
    this.actor = actor;
  }

  delete(actor: Actor): void {
    if (confirm('Are you sure you want to delete this actor?')) {
      this.actorService.deleteActor(actor.id)
        .subscribe(() => {
          this.actors = this.actors.filter(x => x.id != actor.id)
        });
    }
  }

  save(): void {
    if (this.actor.id == 0) {
      if (confirm('Save new actor?')) {
        this.actorService.addActor(this.actor)
          .subscribe({
            next: (x) => {
              this.actors.push(x);
              this.actor = this.actorObject();
            },
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
    else {
      if (confirm('Update actor with ID ' + this.actor.id + '?')) {
        this.actorService.updateActor(this.actor.id, this.actor)
          .subscribe({
            error: (err) => {
              console.log(err);
            }
          });
      }
    }
  }

  cancel(): void {
    this.actor = this.actorObject();
  }

  actorObject(): Actor {
    return { id: 0, name: '', imdbLink: '' }
  }
}