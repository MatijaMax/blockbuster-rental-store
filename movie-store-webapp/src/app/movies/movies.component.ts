import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { MovieClient, Movie } from 'src/app/api/api-reference';
import { Subscription } from 'rxjs';
import { AuthService } from '../api/services/auth.service';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnDestroy {
  movies: Movie[] = [];
  private subscriptions: Subscription[] = [];
  role? = 0;

  constructor(private movieClient: MovieClient, private router: Router, private authorizationService : AuthService) {
    this.loadMovies();
  }

  loadMovies() {
    const subscription = this.movieClient.getAllMovies().subscribe(
      (data) => {
        this.movies = data;
      },
      (error) => {
        console.error('Error loading movies:', error);
      }
    ); 
    // Add the subscription to the list of subscriptions
    this.subscriptions.push(subscription);
    this.authorizationService.getCustomer().then((customer) => {
      if (customer) {
        this.role = customer.role || 0;
      }
    });
    
  }

  deleteMovie(id: string) {
    const subscription = this.movieClient.deleteMovie(id).subscribe(
      () => {
        console.log('Movie deleted successfully.');
        // After successful deletion, reload the movies list to reflect the changes
        this.loadMovies();
      },
      (error) => {
        console.error('Error deleting movie:', error);
      }
    );
    
    // Add the subscription to the list of subscriptions
    this.subscriptions.push(subscription);
  }

  ngOnDestroy() {
    // Unsubscribe from all subscriptions to avoid memory leaks when the component is destroyed
    this.subscriptions.forEach((subscription) => {
      subscription.unsubscribe();
    });
  }
}
