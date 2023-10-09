import { Component, OnDestroy } from '@angular/core';
import { CustomerClient, Customer } from 'src/app/api/api-reference';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DatePipe } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { EditCustomerComponent } from '../edit-customer/edit-customer.component';
import { MsalService } from '@azure/msal-angular';


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnDestroy{
  customers: Customer[] = [];
  selectedCustomerId: string ='';
  private subscriptions: Subscription[] = [];
  displayedColumns: string[] = ['email', 'status', 'role', 'expirationDate', 'purchasedMovies', 'customerMenu'];

  constructor(private customerClient: CustomerClient, private router: Router, private datePipe: DatePipe, public dialog:MatDialog, private authService: MsalService) {

    this.loadCustomers();
  }

  editCustomer(customerId: string): void {
    const dialogRef = this.dialog.open(EditCustomerComponent, {
      width: '400px',
    });

    dialogRef.componentInstance.customerId = customerId;

    dialogRef.afterClosed().subscribe(result => {
        this.loadCustomers();

    });
  }
  loadCustomers() {
    const subscription = this.customerClient.getAllCustomers().subscribe(
      (data) => {
        this.customers = data;
      },
      (error) => {
        console.error('Error loading customers:', error);
      }
    );
    this.subscriptions.push(subscription);
  }

  deleteCustomer(id : string){
    const subscription = this.customerClient.deleteCustomer(id).subscribe(
      () => {
        console.log('Customer deleted successfully!');
        this.loadCustomers();
      },
      (error) => {
        console.error('Error deleting customer', error);
      }


      
    );
    this.subscriptions.push(subscription);

  }

  promoteCustomer(id : string){
    const subscription = this.customerClient.promoteCustomer(id).subscribe(
      () => {
        console.log('Customer promoted successfully!');
        this.loadCustomers();
      },
      (error) => {
        console.error('Error promoting customer', error);
      }
    
    );
    this.subscriptions.push(subscription);

  }


  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }

}
