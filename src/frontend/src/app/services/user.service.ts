import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UserProfile } from '../interfaces/user/user-profile';
import { UserUpdateRequest } from '../interfaces/user/user-update-request';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class UserService {

  constructor(private http: HttpClient) {}

  getUser() {

    return this.http.get<UserProfile>(
      `${environment.apiUrl}/user/me`
  )}
  
  updateUser(body: UserUpdateRequest) {
    return this.http.put<UserProfile>(
      `${environment.apiUrl}/user/me`,
      body
    )
  }

  deleteUser(password:string) {
    return this.http.delete(
      `${environment.apiUrl}/user/me`,
    {
      body: { password }
    }
  )
  }
}
