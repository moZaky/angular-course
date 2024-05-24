import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { IAddMovie, IMovie } from '../models/movie';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  isLoading: boolean = true;
  //@ts-ignore
  selectedMovie$: BehaviorSubject<IMovie | undefined> = new BehaviorSubject(
    undefined
  );
  movieAddedOrUpdated$: BehaviorSubject<boolean> = new BehaviorSubject(false);
  realTimeMovie: BehaviorSubject<IMovie> = new BehaviorSubject<IMovie>({} as IMovie)

  constructor(private http: HttpClient) {
    // this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('/Notify').configureLogging(LogLevel.Information).build();
    // this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
    // this.hubConnectionBuilder.on('NotifyAboutNewMovie', (result: IMovie) => {
    //     this.realTimeMovie.push(result);
    //     console.log({...this.realTimeMovie})
    // });
  }
  //@ts-ignore
  private hubConnection: signalR.HubConnection;
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5057/Notify',{
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  public addNotifyAboutNewMovieListener = () => {
    this.hubConnection.on('NotifyAboutNewMovie', (result: IMovie) => {
      this.realTimeMovie.next(result);
      console.log(result);
    });
  };
  setSelectedMovie(movie: IMovie) {
    this.selectedMovie$.next(movie);
  }
  getMovies(
    pageNumber: number = 1,
    pageSize: number = 7
  ): Observable<IMovie[]> {
    return this.http.get<IMovie[]>(
      `/movies/list?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }
  getMovieById(id: string): Observable<IMovie> {
    return this.http.get<IMovie>(`movies/movie?id=${id}`);
  }
  addMovie(movie: IAddMovie): Observable<boolean> {
    return this.http.post<boolean>(`movies/`, movie);
  }
  editMovie(movie: IMovie): Observable<boolean> {
    return this.http.put<boolean>(`movies/`, movie);
  }
  deleteMovie(movieId: string): Observable<boolean> {
    // let headers:HttpHeaders= new HttpHeaders();
    // headers.append("id",movieId)
    return this.http.delete<boolean>(`movies?movieId=${movieId}`);
  }
}
