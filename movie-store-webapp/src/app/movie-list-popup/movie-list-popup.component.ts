import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Movie } from '../api/api-reference';

@Component({
  selector: 'app-movie-list-popup',
  templateUrl: './movie-list-popup.component.html',
})
export class MovieListPopupComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public purchasedMovies: Movie[]) {
    console.log(this.purchasedMovies);
  }
}
