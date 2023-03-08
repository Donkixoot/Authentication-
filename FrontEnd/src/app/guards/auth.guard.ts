import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, CanActivate } from '@angular/router';
import { AuthService } from '../services/auth.service';


@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) { }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean | UrlTree {
        const isAuthenticated = this.authService.isAuthenticated();
        if (isAuthenticated) {
          return true;
        } else {
          return this.router.parseUrl('/login');
        }
    }
}
