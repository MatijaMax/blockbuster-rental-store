import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    console.log(this.authService.getCustomer());
    const isAuth = await this.authService.isAuthenticated();
    const storedIsLoggedIn = localStorage.getItem('isLoggedIn');
    const storedUserRole = localStorage.getItem('userRole');
    console.log(isAuth);
    console.log("OHO");
    console.log(storedIsLoggedIn);
    if (storedIsLoggedIn === 'true') {
      return true;
    } else {
      this.router.navigate(['auth']);
      return false;
    }
  }
}
