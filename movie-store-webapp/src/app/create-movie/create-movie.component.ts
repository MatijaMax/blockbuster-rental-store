import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieClient, Movie, LicensingType } from 'src/app/api/api-reference';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-create-movie',
  templateUrl: './create-movie.component.html',
  styleUrls: ['./create-movie.component.css']
})
export class CreateMovieComponent implements OnInit, OnDestroy {
  movieForm: FormGroup;
  private subscriptions: Subscription[] = [];
  private isUpdating = false;
  ngSelect: number = 0;

  constructor(
    private formBuilder: FormBuilder,
    private movieClient: MovieClient,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.movieForm = this.formBuilder.group({
      title: ['', Validators.required],
      year: [null, [Validators.required, Validators.pattern(/^[0-9]{4}$/)]],
      licensingType: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isUpdating = true;
        this.loadMovieData(params['id']);
      }
    });
  }

  loadMovieData(movieId: string): void {
    const subscription = this.movieClient.getMovie(movieId).subscribe(
      (movie: Movie) => {
        this.movieForm.patchValue({
          title: movie.title,
          year: movie.year,
          licensingType: movie.licensingType,
        });
      },
      (error) => {
        console.error('Error fetching movie data:', error);
      }
    );

    this.subscriptions.push(subscription);
  }

  onSubmit() {
    if (this.movieForm.valid) {
      const formValues = this.movieForm.value;
      if (this.isUpdating) {
        const movieData = {
          id: this.route.snapshot.params['id'],
          title: formValues.title,
          year: formValues.year,
          licensingType: formValues.licensingType,
        };
        this.updateMovie(movieData);
      } else {
        const movieData = {
          title: formValues.title,
          year: formValues.year,
          licensingType: formValues.licensingType,
        };
        this.createMovie(movieData);
      }
    }
  }

  createMovie(movieData: any): void {
    const subscription = this.movieClient.createMovie(movieData)
      .subscribe(
        () => {
          console.log('Movie created successfully.');
          this.router.navigate(['/movies']);
        },
        (error) => {
          console.error('Error creating movie:', error);
        }
      );

    this.subscriptions.push(subscription);
  }

  updateMovie(movieData: any): void {
    const subscription = this.movieClient.updateMovie(movieData)
      .subscribe(
        () => {
          console.log('Movie updated successfully.');
          this.router.navigate(['/movies']);
        },
        (error) => {
          console.error('Error updating movie:', error);
        }
      );

    this.subscriptions.push(subscription);
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }
}