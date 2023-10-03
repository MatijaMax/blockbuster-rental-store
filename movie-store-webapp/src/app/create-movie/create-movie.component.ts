import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieClient, Movie } from 'src/app/api/api-reference';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs'; // Import Subscription from 'rxjs'

// Import CreateMovieCommand from your API reference (if it's not already imported)
import { CreateMovieCommand } from 'src/app/api/api-reference';

@Component({
  selector: 'app-create-movie',
  templateUrl: './create-movie.component.html',
  styleUrls: ['./create-movie.component.css']
})
export class CreateMovieComponent {
  movieForm: FormGroup;
  ngSelect = 0;
  private subscriptions: Subscription[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private movieClient: MovieClient,
    private router: Router
  ) {
    this.movieForm = this.formBuilder.group({
      title: ['', Validators.required],
      year: [null, [Validators.required, Validators.pattern(/^[0-9]{4}$/)]],
      licensingType: ['None', Validators.required],
    });
  }

  onSubmit() {
    if (this.movieForm.valid) {
      // Access the form values from movieForm
      const formValues = this.movieForm.value;
      console.log('Form submitted:', formValues);

      // Create a CreateMovieCommand object
      const createMovieCommand = new CreateMovieCommand( {
        title: formValues.title,
        year: formValues.year,
        licensingType: formValues.licensingType,
      });

      console.log('CreateMovieCommand object:', createMovieCommand);

      const subscription = this.movieClient.createMovie(createMovieCommand).subscribe(
        () => {
          console.log('Movie created successfully.');
          this.router.navigate(['/movies']);
        },
        (error) => {
          console.error('Error creating movie:', error);
        }
      );

      // Add the subscription to your subscriptions array to manage it
      this.subscriptions.push(subscription);
    } else {
      // Form is invalid; display error messages or handle as needed.
    }
  }
}








