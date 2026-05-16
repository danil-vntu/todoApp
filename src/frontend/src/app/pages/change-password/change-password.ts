import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { AuthService } from '../../services/auth.service';
import { getErrorMessage } from '../../utils/http-error-message';

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
  isSubmitting = false
  changePasswordResponse=signal("")
  errorMessage=signal("")

  isFormInvalid() {
    return this.oldPassword.length < 8 || this.newPassword.length < 8;
  }

  changePassword() {
    if (this.isSubmitting || this.isFormInvalid()) return;

    this.isSubmitting = true;
    this.errorMessage.set("");
    this.changePasswordResponse.set("");

    const body = {
      oldPassword: this.oldPassword,
      newPassword: this.newPassword
    }

    this.authService.changePassword(body)
    .pipe(finalize(() => this.isSubmitting = false))
    .subscribe({
      next: (response) => {
        console.log(response);
        this.changePasswordResponse.set(response);
        this.oldPassword="";
        this.newPassword="";
      },
      error: (error) => {
        console.log(error);
        this.errorMessage.set(getErrorMessage(error));
      }
    })
  }
}
