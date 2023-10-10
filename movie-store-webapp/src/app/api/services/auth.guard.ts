import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    const customer = await this.authService.getCustomer();
    console.log(customer);
    const isAuth = await this.authService.isAuthenticated();
    console.log("OHO");
    console.log(isAuth);
    console.log("OHO");
    if (isAuth) {
      if (customer) {
        const role = customer.role;
        console.log(`Role: ${role}`);
        console.log(`URL: ${state.url}`);
        // Allow access to all routes for role 1
        if (role === 1) {
          return true;
        }
        // For role 0, allow access to specific routes only
        if (role === 0) {
          const allowedRoutes: string[] = ['/movies']; // Replace with your allowed routes
          if (allowedRoutes.includes(state.url)) {
            return true;
          } else {
            // Redirect to a different route if the user is not allowed
            this.router.navigate(['auth']); 
            return false;
          }
        }
      }
    }
    this.router.navigate(['auth']);
    return false;
  }
}
