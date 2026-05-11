import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  email=""
  password=""

  constructor(private http: HttpClient) {}

  logIn() {
    const body = {
      email: this.email,
      password: this.password
    }

    this.http.post(
    'https://localhost:7178/api/auth/login',
    body
    )
    .subscribe({
      next: (response) => {
        console.log("SUCCESS");
        console.log(response);
      },
      error: (error) => {
        console.log("ERROR");
        console.log(error);
      }
    })
  }
}
