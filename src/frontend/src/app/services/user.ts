import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserProfile } from '../interfaces/user/user-profile';

@Injectable({
  providedIn: 'root',
})

export class UserService {

  constructor(private http: HttpClient) {}

  token = localStorage.getItem("token");

  getUser() {

    return this.http.get<UserProfile>(
      "https://localhost:7178/api/user/me",
    {
      headers:
      {
        Authorization: `Bearer ${this.token}`
      }
    }
  )}  
}
