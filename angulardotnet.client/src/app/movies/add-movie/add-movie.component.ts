import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MovieService } from '../movie.service';
import { Router } from '@angular/router';
import { IMovie } from 'src/app/models/movie';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css'],
})
export class AddMovieComponent {
  movieForm = new FormGroup({
    movieName: new FormControl(''),
    imdbId: new FormControl(''),
    poster: new FormControl(''),
    year: new FormControl(''),
  });
  error: string = '';
  constructor(private _movieService: MovieService, private _router: Router) {}

  onSubmit() {
    this.error = '';
    if (!this.movieForm.valid) {
      this.error = 'form invalid';
    }
    this._movieService
      .addMovie(this.movieForm.value as IMovie)
      .subscribe((result) => {
        if (result) {
          this._router.navigate(['']);
          return;
        }
        this.error = 'something went wrong';
      });
  }
}
