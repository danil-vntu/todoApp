import { Component, OnInit, signal } from '@angular/core';

import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserService } from '../../services/user';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [DatePipe, FormsModule, RouterLink],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {

  constructor(
    private userService: UserService) {}

  name=signal("")
  email=signal("")
  createdAt=signal("")

  newName=""

  ngOnInit() {
    console.log('PROFILE INIT');
    this.userService.getUser()
    .subscribe({
      next: (response) => {
        this.name.set(response.name);
        this.email.set(response.email);
        this.createdAt.set(response.createdAt);

        console.log(response);
        console.log(localStorage.getItem('token'));
      },
      error: (error) => {
        console.log(error);
        
      }
    })
  }

  updateUser() {
    const body = {
      name: this.newName
    }

    this.userService.updateUser(body)
    .subscribe({
      next: (response) => {
        console.log(response);
        this.newName="";
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
      }
    })
  }
}
