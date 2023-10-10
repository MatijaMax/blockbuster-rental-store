import { Injectable } from '@angular/core';
import { Customer, CustomerClient } from '../api-reference';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private customer: Customer | null = null;
  isAuth = false;

  constructor(private authService: MsalService, private customerClient: CustomerClient) {
    const accounts = this.authService.instance.getAllAccounts();
    if (accounts.length > 0) {
      this.customerClient.createCustomer().toPromise()
        .then((data: Customer | undefined) => {
          if (data !== undefined) {
            this.customer = data;
            console.log(this.customer);
            this.isAuth = true;
          }
        })
        .catch((error) => {
          console.error('Error fetching customer data:', error);
        });
    }
  }

  isAuthenticated(): Promise<boolean> {
    return Promise.resolve(this.isAuth);
  }

  async getCustomer(): Promise<Customer | null> {
    if (this.customer !== null) {
      return Promise.resolve(this.customer);
    } else {
      try {
        const data = await this.customerClient.createCustomer().toPromise();
        this.customer = data as Customer; // Explicitly specify the type here
        return Promise.resolve(this.customer);
      } catch (error) {
        console.error('Error fetching customer data:', error);
        return Promise.resolve(null);
      }
    }
  }
}
