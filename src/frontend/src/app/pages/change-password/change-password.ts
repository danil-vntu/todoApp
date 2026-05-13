import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-change-password',
  imports: [FormsModule],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css',
})
export class ChangePassword {

  constructor(private authService: AuthService) {}

  oldPassword= ""
  newPassword= ""
  changePasswordResponse=signal("")
  errorMessage=signal("")

  changePassword() {
    const body = {
      oldPassword: this.oldPassword,
      newPassword: this.newPassword
    }

    this.authService.changePassword(body)
    .subscribe({
      next: (response) => {
        console.log(response);
        this.changePasswordResponse.set(response);
        this.oldPassword="";
        this.newPassword="";
      },
      error: (error) => {
        console.log(error);
        this.errorMessage.set(error.error.message);
      }
    })
  }
}
