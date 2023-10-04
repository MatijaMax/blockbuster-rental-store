import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { MoviesComponent } from './movies/movies.component';
import { CreateMovieComponent } from './create-movie/create-movie.component';
import { EditCustomerComponent } from './edit-customer/edit-customer.component';

const routes: Routes = [
  
  { path: '', redirectTo: 'movies', pathMatch: 'full' },
  { path:'customers', component: CustomersComponent },
  { path:'movies', component: MoviesComponent },
  { path:'movies/create', component : CreateMovieComponent},
  { path:'movies/update/:id', component : CreateMovieComponent}, 
  { path:'customers/edit', component : EditCustomerComponent},
];



@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
