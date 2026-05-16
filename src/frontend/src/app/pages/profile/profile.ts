import { Component, OnInit, signal } from '@angular/core';

import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { UserService } from '../../services/user';
import { RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { getErrorMessage } from '../../utils/http-error-message';

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
  isSubmitting = false
  errorMessage=signal("")

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

  isNameInvalid() {
    return this.newName.trim().length === 0 || this.newName.length > 100;
  }

  updateUser() {
    if (this.isSubmitting || this.isNameInvalid()) return;

    this.isSubmitting = true;
    this.errorMessage.set("");

    const body = {
      name: this.newName
    }

    this.userService.updateUser(body)
    .pipe(finalize(() => this.isSubmitting = false))
    .subscribe({
      next: (response) => {
        console.log(response);
        this.newName="";
        window.location.reload();
      },
      error: (error) => {
        console.log(error);
        this.errorMessage.set(getErrorMessage(error));
      }
    })
  }
}
