import { Component } from '@angular/core';
import { CustomerClient, Customer } from 'src/app/api/api-reference';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent {
  customers: Customer[] = [];
  displayedColumns: string[] = ['email', 'status', 'role', 'expirationDate', 'purchasedMovies'];
  constructor(private customerClient: CustomerClient){

    this.customerClient.getAllCustomers().subscribe((data: Customer[]) => {
    this.customers = data;
    console.log(this.customers);
  });
  }
}
