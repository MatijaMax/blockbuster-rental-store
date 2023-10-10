import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { InteractionStatus, PopupRequest, RedirectRequest } from '@azure/msal-browser';
import { Observable, Subject } from 'rxjs';
import { CustomerClient, Customer } from 'src/app/api/api-reference';
import { filter, takeUntil } from 'rxjs/operators';
import { AuthService } from '../api/services/auth.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  loginDisplay = false;
  username = '';
  role? =0;
  status? =0;
  id? ='';
  loggedCustomer: Observable<Customer>[]= [];

  constructor(private customerClient: CustomerClient, private authService: MsalService, private authorizationService : AuthService) { }
  login() {
    this.authService.loginPopup()
      .subscribe({
        next: () => {
          this.setLoginDisplay(); // Update login display
        },
        error: (error) => console.log(error)
      });
  }

  logout() { // Add log out function here
    this.authService.logoutPopup({
      mainWindowRedirectUri: "/"
    });
  }

  setLoginDisplay() {
    const accounts = this.authService.instance.getAllAccounts();
    this.loginDisplay = accounts.length > 0;
    this.username = accounts[0].username;
    if (accounts.length > 0) {
      this.customerClient.createCustomer().subscribe(
        (data: Customer) => {
          this.role = data.role || 0;
          this.status = data.status || 0; 
          this.id = data.id || ''; 
        },
        (error) => {
          console.error('Error fetching customer data:', error);
        }
      );
    console.log(this.authorizationService.getCustomer());
  }
}
}

