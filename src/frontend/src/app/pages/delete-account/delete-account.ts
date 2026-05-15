import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { UserService } from '../../services/user';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-delete-account',
  imports: [FormsModule, RouterLink],
  templateUrl: './delete-account.html',
  styleUrl: './delete-account.css',
})
export class DeleteAccount {

  constructor(
    private userService: UserService,
    private router: Router) {}

  password= ""
  errorMessage=signal("")

  deleteAccount() {
    this.userService.deleteUser(this.password)
    .subscribe({
      next: () => {
        console.log("DELETED!");
        this.password="";
        localStorage.removeItem("token")
        this.router.navigate(["/login"])
      },
      error: (error) => {
        console.log(error);
        this.errorMessage.set(error.error.message);
      }
    })
  }
}
