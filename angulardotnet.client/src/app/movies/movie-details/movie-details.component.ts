import { Component } from '@angular/core';
import { MovieService } from '../movie.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IMovie } from 'src/app/models/movie';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent {
  isLoading: boolean = false;
  movie: IMovie = {} as IMovie;
  $moviesObs: Observable< IMovie>= new Observable()
  imdbID:string
  constructor(
    private _movieService: MovieService,
    private route: ActivatedRoute,
    private router: Router 
  ) {

    this.imdbID = this.route.snapshot.params['id']
     
  }
  ngOnInit(): void {
    this.isLoading = true;
    this.$moviesObs = this._movieService.getMovieById( this.imdbID)
    this._movieService.getMovieById( this.imdbID).subscribe((result) => {
    
      if (result) {
        this.movie=result;
      }
      this.isLoading = false;
    });

    
  }
}
