import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AuthResponse } from '../interfaces/auth/auth-response';
import { LoginRequest } from '../interfaces/auth/login-request';
import { RegisterRequest } from '../interfaces/auth/register-request';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient) {}

  register(body: RegisterRequest) {
    return this.http.post<AuthResponse>(
    'https://localhost:7178/api/auth/register',
    body
    )
  }

  login(body: LoginRequest) {
    return this.http.post<AuthResponse>(
    'https://localhost:7178/api/auth/login',
    body
    )
  }
}
