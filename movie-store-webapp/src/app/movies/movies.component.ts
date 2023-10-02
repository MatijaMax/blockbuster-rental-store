import { Component } from '@angular/core';
import { MovieClient, Movie} from 'src/app/api/api-reference';
@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent {
  movies: Movie[] = [];
  constructor(private movieClient: MovieClient){

    this.movieClient.getAllMovies().subscribe((data: Movie[]) => {
    this.movies = data;
    console.log(this.movies);
  });
  }
}
