import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { MoviesComponent } from './movies/movies.component';
import { CreateMovieComponent } from './create-movie/create-movie.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';
import { AuthComponent } from './auth/auth.component';
import { BrowserUtils } from "@azure/msal-browser";
import { MsalGuard } from '@azure/msal-angular';


const routes: Routes = [

  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'customers', component: CustomersComponent },
  { path: 'movies', component: MoviesComponent },
  { path: 'movies/create', component: CreateMovieComponent },
  { path: 'movies/update/:id', component: CreateMovieComponent },
  { path: 'customers/edit', component: EditCustomerComponent },
  { path: 'auth', component: AuthComponent }
];

const isIframe = window !== window.parent && !window.opener;

@NgModule({
  imports: [RouterModule.forRoot(routes,
    {
      initialNavigation:
        !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup()
          ? "enabledNonBlocking"
          : "disabled", // Set to enabledBlocking to use Angular Universal 

    }),
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
