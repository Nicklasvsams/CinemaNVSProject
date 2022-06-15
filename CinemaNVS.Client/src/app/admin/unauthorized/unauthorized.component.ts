import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})
export class UnauthorizedComponent implements OnInit {
  counter: number = 5;

  constructor(private route: Router) { }

  ngOnInit(): void {
    this.countdown();
  }

  countdown(): void {
    setInterval(() => {
      this.counter = this.counter - 1;
      if (this.counter == 0) {
        this.route.navigate(['']);
      }
    }, 1000)

  }
}
