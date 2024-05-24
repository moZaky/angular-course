using AngularDotnet.Core;
using AngularDotnet.Core.DTOs;
using AngularDotnet.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularDotnet.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private MovieCatalogDbContext _dbContext;
        private IMapper _mapper;
        public MoviesController(MovieCatalogDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<MovieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<MovieDTO>), StatusCodes.Status400BadRequest)]
        [Route("list")]
        public async Task<ActionResult> GetAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            try
            {
                var movies = await _dbContext.Movies

               .AsNoTracking()
               //.Chunk(pageSize)
               .Where(e => e.IsActive)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
                var result = _mapper.Map<List<MovieDTO>>(movies);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

        }

        [HttpGet]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [Route("movie/name")]
        public async Task<ActionResult> GetMovieByName(string name)
        {
            if (name is null) return BadRequest("name cannot be empty, its required to search");


            var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => string.Equals(movie.MovieName, name, StringComparison.OrdinalIgnoreCase));
            return movie is not null ? Ok(movie) : BadRequest($"movie not found with name {name}");
        }
        [HttpGet]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status200OK)]
        [Route("movie")]
        public async Task<ActionResult> GetMovieById([FromQuery] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("id cannot be empty");
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id && movie.IsActive);

            return movie is not null ? Ok(movie) : BadRequest($"movie not found with id {id}");
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] Movies movie)
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

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> Update([FromBody] Movies movie)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var entity = await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == movie.Id);
                if (entity == null) return BadRequest("cannot find movie ");

                entity.MovieName = movie.MovieName;
                entity.IMDBId = movie.IMDBId;
                entity.Poster = movie.Poster;
                entity.Year = movie.Year;

                _dbContext.Movies.Update(entity);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? Ok(true) : BadRequest("cannot update movie");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

        }
        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromQuery] Guid movieId)
        {
            try
            {
                if (movieId == Guid.Empty) return BadRequest("movieId is empty");
                var entity = await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == movieId);
                if (entity == null) return BadRequest("cannot find movie ");

                entity.IsActive = false;
                //  _dbContext.Movies.Remove(entity);//hard delete
                _dbContext.Movies.Update(entity);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? Ok(true) : BadRequest("cannot delete movie");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }

        }
    }

}
