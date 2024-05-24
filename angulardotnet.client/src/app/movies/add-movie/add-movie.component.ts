import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MovieService } from '../movie.service';
import { Router } from '@angular/router';
import { IAddMovie, IMovie } from 'src/app/models/movie';
// import * as logger  from "../../../assets/seo.js"
declare var showOnPage: any;

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css'],
})
export class AddMovieComponent implements OnInit, OnDestroy {
  movieForm: FormGroup;

  isEdit: boolean = false;
  error: string = '';

  constructor(private _movieService: MovieService, private _router: Router) {
    this.movieForm = this.initForm();
  }
  ngOnDestroy(): void {
    this.movieForm = this.initForm();
    this.isEdit = false;
    this._movieService.selectedMovie$.next(undefined)
  }

  get movieId() {
    return this.movieForm.get('id')?.value;
  }
  ngOnInit(): void {
    this._movieService.selectedMovie$.subscribe((movie) => {
      if(movie){
        this.isEdit = true;

        this.movieForm.patchValue(movie);
      }
      
    });
  }
  showListing(isSaved: boolean) {
    this._movieService.movieAddedOrUpdated$.next(isSaved);
    this._router.navigate(['']);
  }
  onSubmit() {
    this.error = '';
    if (!this.movieForm.valid) {
      this.error = 'form invalid';
    }
    this.isEdit ? this.edit() : this.add();
  }
  add() {
    const { id, ...rest } = this.movieForm.value;
    console.log(rest);
    this._movieService.addMovie(rest as IMovie).subscribe((result) => {
      if (result) {
        this.showListing(result);
        return;
      }
      this.error = 'something went wrong';
    });
  }
  edit() {
    this._movieService
      .editMovie(this.movieForm.value as IMovie)
      .subscribe((result) => {
        if (result) {
          this.showListing(result);

          return;
        }
        this.error = 'something went wrong';
      });
  }
  initForm() {
    return new FormGroup({
      movieName: new FormControl(''),
      imdbId: new FormControl(''),
      poster: new FormControl(''),
      year: new FormControl(''),
      id: new FormControl(''),
    });
  }
}
