import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MovieClient, Movie } from 'src/app/api/api-reference';
@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent {
  movies: Movie[] = [];
  constructor(private movieClient: MovieClient, private router: Router) {

    this.movieClient.getAllMovies().subscribe(data => this.movies = data);
  }

  createMovie() {
    // Code to execute when the button is clicked
    this.router.navigate(['/movies/create']);
  }

}
