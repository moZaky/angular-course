import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { Observable } from 'rxjs';
import { IMovie } from 'src/app/models/movie';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent implements OnInit {
  movies$: Observable<IMovie[]> = new Observable();
  showForm=false;
  constructor(private _movieService: MovieService) {}
  ngOnInit(): void {
    this.movies$=this._movieService.getMovies();
  }
  
}
