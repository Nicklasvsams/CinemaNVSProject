import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { LoginComponent } from './admin/login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Cinema NVS';
  sessionUsername: any = null;
  sessionRole: any = null;

  constructor(private route: Router) { }

  ngOnInit(): void {
    this.sessionUsername = sessionStorage?.getItem('user');
    this.sessionRole = sessionStorage?.getItem('role');

    console.log(this.sessionUsername);

    window.onclick = (event: Event) => {
      if ((event.target as HTMLButtonElement) != document.getElementById('dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (let i = 0; i < dropdowns.length; i++) {
          var openDropdown = dropdowns[i];
          if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
          }
        }
      }
    }
  }

  modifySessionInfo(): void {
    this.sessionUsername = sessionStorage.getItem('user');
    this.sessionRole = sessionStorage.getItem('role');
  }

  logOut(): void {
    sessionStorage.clear();
    this.sessionUsername = null;
    this.sessionRole = null;
    this.route.navigate(['']);
  }

  /* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
  dropDownNav() {
    const dropDown = document.getElementById("mydropdown");

    if (dropDown !== null) {

      dropDown.classList.toggle("show");
    }
  }
}