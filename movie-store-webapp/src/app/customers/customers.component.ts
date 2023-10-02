import { Component } from '@angular/core';
import { CustomerClient, Customer } from 'src/app/api/api-reference';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent {
  customers: Customer[] = [];
  displayedColumns: string[] = ['email', 'status', 'role', 'expirationDate', 'purchasedMovies', 'customerMenu'];
  constructor(private customerClient: CustomerClient, private router: Router) {
    this.customerClient.getAllCustomers().subscribe((data: Customer[]) => this.customers = data);
  }

  editCustomer() {
    // Code to execute when the button is clicked
    this.router.navigate(['/customers/edit']);
  }

}
