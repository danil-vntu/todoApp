import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';

import { UserService } from '../../services/user';
import { Router } from '@angular/router';
import { getErrorMessage } from '../../utils/http-error-message';

@Component({
  selector: 'app-delete-account',
  imports: [FormsModule],
  templateUrl: './delete-account.html',
  styleUrl: './delete-account.css',
})
export class DeleteAccount {

  constructor(
    private userService: UserService,
    private router: Router) {}

  password= ""
  isSubmitting = false
  errorMessage=signal("")

  isFormInvalid() {
    return this.password.length < 8;
  }

  deleteAccount() {
    if (this.isSubmitting || this.isFormInvalid()) return;

    this.isSubmitting = true;
    this.errorMessage.set("");

    this.userService.deleteUser(this.password)
    .pipe(finalize(() => this.isSubmitting = false))
    .subscribe({
      next: () => {
        console.log("DELETED!");
        this.password="";
        localStorage.removeItem("token")
        this.router.navigate(["/login"])
      },
      error: (error) => {
        console.log(error);
        this.errorMessage.set(getErrorMessage(error));
      }
    })
  }
}
