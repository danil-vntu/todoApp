import { Component, OnInit, signal } from '@angular/core';

import { UserService } from '../../services/user';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {

  constructor(private userService: UserService) {}

  name=signal("")
  email=signal("")
  createdAt=signal("")

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
}
