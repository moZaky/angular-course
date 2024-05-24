using AngularDotnet.Core;
using AngularDotnet.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AngularDotnet.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private MovieCatalogDbContext _dbContext;
        public List<MovieDTO> movies = new()
        {
            new ()
                {
                    IMDBId = "123",
                    MovieName = "Goldfinger",
                    Poster = "https://th.bing.com/th/id/OIP.Hfk1jM2wyz7Nh_5pMxR-DwHaFK?rs=1&pid=ImgDetMain",
                    Year = new DateOnly(1979, 1, 1).ToString()
                },
            new()
            {
                IMDBId = "456",
                MovieName = "GoldenEye",
                Poster = "https://th.bing.com/th/id/OIP.Hfk1jM2wyz7Nh_5pMxR-DwHaFK?rs=1&pid=ImgDetMain",
                Year = new DateOnly(1979, 1, 1).ToString()
            },
            new()
                {
                    IMDBId = "897",
                    MovieName = "Casino Royale",
                    Poster = "https://th.bing.com/th/id/OIP.Hfk1jM2wyz7Nh_5pMxR-DwHaFK?rs=1&pid=ImgDetMain",
                    Year = new DateOnly(1979, 1, 1).ToString()
                }
        };
        public MoviesController(MovieCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<MovieDTO>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {

            return Ok(movies);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [Route("movie/name")]
        public ActionResult GetMovieByName(string name)
        {
            if (name is null) return BadRequest("name cannot be empty, its required to search");


            var movie = movies.FirstOrDefault(movie => string.Equals(movie.MovieName, name, StringComparison.OrdinalIgnoreCase));
            return movie is not null ? Ok(movie) : BadRequest($"movie not found with name {name}");
        }
        [HttpGet]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [Route("movie")]
        public ActionResult GetMovieById([FromQuery] string id)
        {
            if (id is null) return BadRequest("id cannot be empty");
            var movie = movies.FirstOrDefault(movie => movie.IMDBId == id);

            return Ok(movie);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] MovieDTO movie)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _dbContext.Movies.Add(new()
            {
                IMDBId = movie.IMDBId,
                MovieName = movie.MovieName,
                Poster = movie.Poster,
                Year = movie.Year,
            });
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? Ok(movie) : BadRequest("errror");
        }

    }

}
