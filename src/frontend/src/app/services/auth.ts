import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AuthResponse } from '../interfaces/auth/auth-response';
import { LoginRequest } from '../interfaces/auth/login-request';
import { RegisterRequest } from '../interfaces/auth/register-request';
import { ChangePassword } from '../interfaces/auth/change-password-request';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient) {}

  register(body: RegisterRequest) {
    return this.http.post<AuthResponse>(
    `${environment.apiUrl}/auth/register`,
    body
    )
  }

  login(body: LoginRequest) {
    return this.http.post<AuthResponse>(
    `${environment.apiUrl}/auth/login`,
    body
    )
  }

  changePassword(body: ChangePassword) {
    return this.http.post(
      `${environment.apiUrl}/auth/change-password`,
      body,
      { responseType: "text"}
    )
  }
}
