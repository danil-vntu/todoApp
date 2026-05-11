import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const GuestGuard: CanActivateFn = () => {

  const router = inject(Router);

  const token = localStorage.getItem("token");

  if(!token) {
    return true;
  }

  router.navigate(['/tasks']);
  return false;
};