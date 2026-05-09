import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Tasks } from './pages/tasks/tasks';
import { Categories } from './pages/categories/categories';
import { Profile } from './pages/profile/profile';

export const routes: Routes = [
  { path: '', redirectTo: 'tasks', pathMatch: 'full' },

  { path: 'login', component: Login },
  { path: 'register', component: Register },

  { path: 'tasks', component: Tasks },
  { path: 'categories', component: Categories },
  { path: 'profile', component: Profile },
  
  { path: '**', redirectTo: 'tasks' }
];
