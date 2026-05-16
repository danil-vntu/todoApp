import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { getErrorMessage } from '../../utils/http-error-message';


@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  constructor(private authService: AuthService,
    private router: Router
  ) {}

  email=""
  name=""
  password=""
  isSubmitting = false

  errorMessage=signal("");

  isEmailInvalid() {
    return this.email.length > 0 && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(this.email);
  }

  isNameInvalid() {
    return this.name.trim().length === 0 || this.name.length > 100;
  }

  isRegisterInvalid() {
    return this.email.length === 0 ||
      this.email.length > 450 ||
      this.isEmailInvalid() ||
      this.isNameInvalid() ||
      this.password.length < 8;
  }

  register() {
    if (this.isSubmitting || this.isRegisterInvalid()) return;

    this.isSubmitting = true;
    this.errorMessage.set("");

    const body = {
      email: this.email,
      name: this.name,
      password: this.password
    }

    this.authService.register(body)
    .pipe(finalize(() => this.isSubmitting = false))
    .subscribe({
      next: (response) => {
        console.log("SUCCESS");
        console.log(response);
        localStorage.setItem("token", response.token);
        this.router.navigate(["/tasks"])
        
        this.email="";
        this.name="";
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
