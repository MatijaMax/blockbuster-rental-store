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
export class NavbarComponent implements OnInit {
  loginDisplay = false;
  username? = '';
  role? =0;
  status? =0;
  id? ='';
  isLogged = false;
  loggedCustomer: Observable<Customer>[]= [];

  constructor(private customerClient: CustomerClient, private authService: MsalService, private authorizationService : AuthService) 
  {
  }

  ngOnInit(): void {
      this.setLoginDisplay();
  } 

  login() {
    this.authService.loginPopup()
      .subscribe({
        next: () => {
          this.authService.instance.setActiveAccount(this.authService.instance.getAllAccounts()[0]);
          this.setLoginDisplay(); // Update login display
          this.isLogged=true;
          localStorage.setItem('isLoggedIn', this.isLogged.toString());
          localStorage.setItem('userRole', this.role ? this.role.toString() : '');
          location.reload();
        },
        error: (error) => console.log(error)
      });
  }

  logout() { // Add log out function here
    localStorage.removeItem('myLoggedIn');
    localStorage.removeItem('userRole');
    this.authService.logoutPopup({
      mainWindowRedirectUri: "/"
      
    });
  }

  setLoginDisplay() {
    const accounts = this.authService.instance.getAllAccounts();
    if(this.loginDisplay = accounts.length > 0){
      this.username = accounts[0].username;

    };
    this.authorizationService.getCustomer().then((customer) => {
      if (customer) {
        this.role = customer.role || 0;
        //this.isLogged=true;
        //localStorage.setItem('isLoggedIn', this.isLogged.toString());
        //localStorage.setItem('userRole', this.role ? this.role.toString() : '');
      }
    });
    this.authorizationService.getCustomer().then((customer) => {
      if (customer) {
        this.role = customer.role || 0;
      }
    });

    console.log(this.authorizationService.getCustomer());
    console.log(accounts.length);
  }
}


