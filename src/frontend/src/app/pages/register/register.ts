import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-login',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  constructor(private authService: AuthService) {}

  email=""
  name=""
  password=""

  errorMessage=signal("");

  register() {
    const body = {
      email: this.email,
      name: this.name,
      password: this.password
    }

    this.authService.register(body)
    .subscribe({
      next: (response) => {
        console.log("SUCCESS");
        console.log(response);
        localStorage.setItem("token", response.token);
        window.location.reload();
        
        this.email="";
        this.name="";
        this.password="";
        this.errorMessage.set("");
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
