import { Component } from '@angular/core';
import { CustomerClient, Customer } from 'src/app/api/api-reference';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'movie-store-webapp';
  customers: Customer[] = [];
  constructor(private customerClient: CustomerClient) {
    
    this.customerClient.getAllCustomers().subscribe((data: Customer[]) => {
    this.customers = data;
    console.log(this.customers);
    });
  }
}

