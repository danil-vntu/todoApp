import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { getErrorMessage } from '../../utils/http-error-message';


@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  constructor(private authService: AuthService,
    private router: Router
  ) {}

  email=""
  password=""
  isSubmitting = false

  errorMessage=signal("");

  isEmailInvalid() {
    return this.email.length > 0 && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.email);
  }

  isLoginInvalid() {
    return this.email.length === 0 ||
      this.email.length > 450 ||
      this.isEmailInvalid() ||
      this.password.length < 8;
  }

  login() {
    if (this.isSubmitting || this.isLoginInvalid()) return;

    this.isSubmitting = true;
    this.errorMessage.set("");

    const body = {
      email: this.email,
      password: this.password
    }

    this.authService.login(body)
    .pipe(finalize(() => this.isSubmitting = false))
    .subscribe({
      next: (response) => {
        console.log("SUCCESS");
        console.log(response);
        localStorage.setItem("token", response.token);
        this.router.navigate(["/tasks"])
        
        this.email="";
        this.password="";
        this.errorMessage.set("");
      },
      error: (error) => {
        console.log("ERROR");
        console.log(error);
        this.errorMessage.set(getErrorMessage(error))
      }
    })
  }
}
