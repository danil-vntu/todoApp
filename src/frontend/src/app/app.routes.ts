import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Tasks } from './pages/tasks/tasks';
import { Categories } from './pages/categories/categories';
import { Profile } from './pages/profile/profile';
import { authGuard } from './guards/auth-guard';
import { guestGuard } from './guards/guest-guard';
import { ChangePassword } from './pages/change-password/change-password';
import { DeleteAccount } from './pages/delete-account/delete-account';

export const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },

  { path: 'login', component: Login, canActivate: [guestGuard] },
  { path: 'register', component: Register, canActivate: [guestGuard] },

  { path: 'tasks', component: Tasks, canActivate: [authGuard] },
  { path: 'categories', component: Categories, canActivate: [authGuard] },
  { path: 'profile', component: Profile, canActivate: [authGuard] },
  { path: 'change-password', component: ChangePassword, canActivate: [authGuard] },
  { path: 'delete-account', component: DeleteAccount, canActivate: [authGuard] },
  
  { path: '**', redirectTo: 'tasks' }
];
