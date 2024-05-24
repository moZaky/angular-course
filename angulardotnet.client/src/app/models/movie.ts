export interface IMovie {
  movieName: string;
  imdbId: string;
  poster: string;
  year: string;
  id: string;
}

export type IAddMovie=  Omit<IMovie,  "id">
  
 