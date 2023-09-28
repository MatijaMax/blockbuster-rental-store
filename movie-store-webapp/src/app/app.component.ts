import { Component, OnInit } from '@angular/core';
import { CustomerClient, Customer } from './api/api_reference';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'movie-store-webapp';
  customers: Customer[] = [];
  
  ngOnInit() {
      CustomerClient.getAllCustomers().subscribe((data: Customer[]) => {
      this.customers = data;
      // Log the list of customers to the console
      console.log(this.customers);
    });
  }
}
