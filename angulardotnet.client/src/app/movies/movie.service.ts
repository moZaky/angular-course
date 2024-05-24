import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IMovie } from '../models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  isLoading:boolean =true;
  constructor(private http: HttpClient) { }

  getMovies(): Observable<IMovie[]> {
   return this.http.get<IMovie[]>("/movies/")
   
  }
  getMovieById(id:string): Observable<IMovie> {
    return this.http.get<IMovie>(`movies/movie?id=${id}`);
   }
   addMovie(movie:IMovie): Observable<boolean> {
    return this.http.post<boolean>(`movies/`,movie);
   }
}
