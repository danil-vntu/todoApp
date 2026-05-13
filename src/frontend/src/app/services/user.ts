import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserProfile } from '../interfaces/user/user-profile';
import { UserUpdate } from '../interfaces/user/user-update-request';

@Injectable({
  providedIn: 'root',
})

export class UserService {

  constructor(private http: HttpClient) {}

  getUser() {

    return this.http.get<UserProfile>(
      "https://localhost:7178/api/user/me"
  )}
  
  updateUser(body: UserUpdate) {
    return this.http.put<UserProfile>(
      "https://localhost:7178/api/user/me",
      body
    )
  }
}
