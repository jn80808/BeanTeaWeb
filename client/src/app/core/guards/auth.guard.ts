import { CanActivateFn } from '@angular/router';
import { inject, Injectable } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';



export const authGuard: CanActivateFn = 
(route: ActivatedRouteSnapshot, 
  state: RouterStateSnapshot): Observable<boolean> => 
    {
      const accountService = inject(AccountService);
      const router = inject(Router);

      return accountService.currentUser$.pipe(
        map(auth => {
          if (auth) return true;
          else {
            router.navigate(['/account/login'], { queryParams: { returnUrl: state.url } });
            return false;
      }
    })
  );
};
