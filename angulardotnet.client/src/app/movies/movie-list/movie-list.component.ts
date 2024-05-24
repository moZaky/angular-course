import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { Observable } from 'rxjs';
import { IMovie } from 'src/app/models/movie';
import { Route, Router } from '@angular/router';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
})
export class MovieListComponent implements OnInit {
  movies$: Observable<IMovie[]> = new Observable();
  showForm = false;
  constructor(private _movieService: MovieService, private _router: Router) {}
  ngOnInit(): void {
    this._movieService.movieAddedOrUpdated$.subscribe((displayForm) => {
      this.showForm = displayForm;
    });
    this.getAllMovies();
    this._movieService.startConnection();
    this._movieService.addNotifyAboutNewMovieListener();
    this._movieService.realTimeMovie.subscribe(movie=>{
      if(movie?.id){
        Swal.fire(`new movie is added ${movie.movieName} `);

      }
    })
  }
  getAllMovies() {
    this.movies$ = this._movieService.getMovies();
  }
  editMovie(movie: IMovie) {
    this._movieService.setSelectedMovie(movie);
    this._router.navigate(['/add-form']);
  }
  confirmDeleteMovie(id: string) {
    Swal.fire({
      title: 'Do you want to save the changes?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Yes',
      denyButtonText: 'No',
      customClass: {
        actions: 'my-actions',
        cancelButton: 'order-1 right-gap',
        confirmButton: 'order-2',
        denyButton: 'order-3',
      },
    }).then((result) => {
      if (result.isConfirmed) {
        this.deleteMovie(id);
      } else if (result.isDenied) {
        Swal.fire('Changes are not saved', '', 'info');
      }
    });
  }
  deleteMovie(id: string) {
    this._movieService.deleteMovie(id).subscribe((res) => {
      if (res) {
        Swal.fire('Deleted!', '', 'success');
         this.getAllMovies()
      }
    });
  }
}
