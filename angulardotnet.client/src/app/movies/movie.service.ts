import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { IAddMovie, IMovie } from '../models/movie';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  isLoading:boolean =true;
  //@ts-ignore
  selectedMovie$:BehaviorSubject<IMovie |undefined> =new BehaviorSubject(undefined);
  movieAddedOrUpdated$:BehaviorSubject<boolean> = new BehaviorSubject(false);
  constructor(private http: HttpClient) { }


  setSelectedMovie(movie:IMovie){
    this.selectedMovie$.next(movie)
  }
  getMovies(pageNumber:number=1,pageSize:number=7): Observable<IMovie[]> {
   return this.http.get<IMovie[]>(`/movies/list?pageNumber=${pageNumber}&pageSize=${pageSize}`)
   
  }
  getMovieById(id:string): Observable<IMovie> {
    return this.http.get<IMovie>(`movies/movie?id=${id}`);
   }
   addMovie(movie:IAddMovie): Observable<boolean> {
    return this.http.post<boolean>(`movies/`,movie);
   }
   editMovie(movie:IMovie): Observable<boolean> {
    return this.http.put<boolean>(`movies/`,movie);
   }
   deleteMovie(movieId:string): Observable<boolean> {
    // let headers:HttpHeaders= new HttpHeaders();
    // headers.append("id",movieId)
    return this.http.delete<boolean>(`movies?movieId=${movieId}`);
   }
}
