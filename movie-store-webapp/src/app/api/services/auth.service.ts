import { Injectable } from '@angular/core';
import { Customer, CustomerClient } from '../api-reference';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private customer: Customer | null = null;
  isAuth=false;

  constructor(private authService: MsalService, private customerClient: CustomerClient) {
    const accounts = this.authService.instance.getAllAccounts();
    if (accounts.length > 0) {
      this.customerClient.createCustomer().subscribe(
        (data: Customer) => {
          this.customer = data;
          this.isAuth = true;
        },
        (error) => {
          console.error('Error fetching customer data:', error);
        }
      );
    }
  }

  isAuthenticated(): boolean {
    return this.isAuth;
  }

  getCustomer(): Customer | null {
    return this.customer;
  }
}

