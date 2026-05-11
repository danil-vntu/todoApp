import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Tasks } from './pages/tasks/tasks';
import { Categories } from './pages/categories/categories';
import { Profile } from './pages/profile/profile';
import { AuthGuard } from './guards/auth-guard';
import { GuestGuard } from './guards/guest-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },

  { path: 'login', component: Login, canActivate: [GuestGuard] },
  { path: 'register', component: Register, canActivate: [GuestGuard] },

  { path: 'tasks', component: Tasks, canActivate: [AuthGuard] },
  { path: 'categories', component: Categories, canActivate: [AuthGuard] },
  { path: 'profile', component: Profile, canActivate: [AuthGuard] },
  
  { path: '**', redirectTo: 'tasks' }
];
