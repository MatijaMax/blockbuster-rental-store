import { Component, OnDestroy } from '@angular/core';
import { CustomerClient, Customer, Movie, MovieClient } from 'src/app/api/api-reference';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DatePipe } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { EditCustomerComponent } from '../edit-customer/edit-customer.component';
import { MsalService } from '@azure/msal-angular';
import { MovieListPopupComponent } from '../movie-list-popup/movie-list-popup.component';


@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnDestroy{
  customers: Customer[] = [];
  movies: Movie[] = [];
  selectedCustomerId: string ='';
  private subscriptions: Subscription[] = [];
  displayedColumns: string[] = ['email', 'status', 'role', 'expirationDate', 'purchasedMovies', 'customerMenu'];

  constructor(private customerClient: CustomerClient, private movieClient : MovieClient, private router: Router, private datePipe: DatePipe, public dialog:MatDialog, private authService: MsalService) {

    this.loadCustomers();
    this.loadMovies();
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

  loadMovies() {
    const subscription = this.movieClient.getAllMovies().subscribe(
      (data) => {
        this.movies = data;
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

  showPurchasedMovies(customer:Customer) {
    // Retrieve the list of purchased movies (you should implement this)
   // const purchasedMovies = this.getPurchasedMovies(customer);
    // Open the pop-up
    this.dialog.open(MovieListPopupComponent, {
     data: this.getPurchasedMovies(customer),
      width: '300px',
      height:'300px',
      panelClass: 'custom-dialog',
    });
    console.log(this.getPurchasedMovies(customer));
  }

  // ...

  getPurchasedMovies(customer: Customer): Movie[] {
    if (!customer.purchasedMovies || customer.purchasedMovies.length === 0) {
      return [];
    } 
    const allMovies: Movie[] = this.movies;
    let purchasedMovies: Movie[] = [];
    for (const purchase of customer.purchasedMovies) {
      if (purchase.movie && purchase.movie.id) {
        purchasedMovies = this.cleanMovies(purchase.movie.id, allMovies, purchasedMovies);
      }
    }
    console.log(purchasedMovies);
    return purchasedMovies;
}

  cleanMovies(cleanId : string, allMovies: Movie[], purchasedMovies: Movie[]) : Movie[]{
    
    for(const movie of allMovies){
      if(movie.id == cleanId){
        purchasedMovies.push(movie);
      }
    }
    return purchasedMovies;
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }

}
