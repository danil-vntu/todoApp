import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})

export class Navbar {
  isAuthenticated() {
    return localStorage.getItem("token") !== null;
  }

  logout() {
    localStorage.removeItem("token");
    window.location.reload();
  }
}
