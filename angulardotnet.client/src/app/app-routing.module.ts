import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MovieDetailsComponent } from './movies/movie-details/movie-details.component';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { AddMovieComponent } from './movies/add-movie/add-movie.component';
 

const routes: Routes = [
  { path: '', component: MovieListComponent,  },
  { path: 'list', component: MovieListComponent,pathMatch:"full" },
  { path: 'details/:id', component: MovieDetailsComponent },
  { path: 'add-form', component: AddMovieComponent },
 
 

];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
