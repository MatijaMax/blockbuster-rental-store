import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { MoviesComponent } from './movies/movies.component';
import { CreateMovieComponent } from './create-movie/create-movie.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { AuthComponent } from './auth/auth.component';
import { BrowserUtils } from "@azure/msal-browser";
import { MsalGuard } from '@azure/msal-angular';
import { AuthGuard } from './api/services/auth.guard';


const routes: Routes = [

  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'customers', component: CustomersComponent, canActivate:[AuthGuard] },
  { path: 'movies', component: MoviesComponent, canActivate:[AuthGuard] },
  { path: 'movies/create', component: CreateMovieComponent, canActivate:[AuthGuard] },
  { path: 'movies/update/:id', component: CreateMovieComponent, canActivate:[AuthGuard] },
  { path: 'customers/edit', component: EditCustomerComponent, canActivate:[AuthGuard] },
  { path: 'auth', component: AuthComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
