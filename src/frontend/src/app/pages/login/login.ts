import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services/auth';


@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  constructor(private authService: AuthService ) {}

  email=""
  password=""

  errorMessage=signal("");

  login() {
    const body = {
      email: this.email,
      password: this.password
    }

    this.authService.login(body)
    .subscribe({
      next: (response) => {
        console.log("SUCCESS");
        console.log(response);
        localStorage.setItem("token", response.token);
      },
      error: (error) => {
        console.log("ERROR");
        console.log(error);
        console.log(error.error.message)
        this.errorMessage.set(error.error.message)
      }
    })
  }
}
