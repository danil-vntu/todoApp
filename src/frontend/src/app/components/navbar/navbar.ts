import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { ThemeService } from '../../services/theme.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {
  isLogoutConfirmationOpen = false;

  constructor(public themeService: ThemeService) {}

  isAuthenticated() {
    return localStorage.getItem('token') !== null;
  }

  isDark() {
    return this.themeService.isDark();
  }

  toggleTheme() {
    this.themeService.toggleTheme();
  }

  requestLogout() {
    this.isLogoutConfirmationOpen = true;
  }

  cancelLogout() {
    this.isLogoutConfirmationOpen = false;
  }

  confirmLogout() {
    localStorage.removeItem('token');
    window.location.reload();
  }
}
