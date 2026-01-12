using Lapostyan_Martin_backend.Models;
using Lapostyan_Martin_backend.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lapostyan_Martin_backend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MovieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("feladat10")]
        public IActionResult GetAllMovie()
        {
            try
            {
                using var context = new CinemadbContext();
                var movies = context.Movies
                    .Select(m => new
                    {
                        movieId = m.MovieId,
                        title = m.Title,
                        releaseDate = m.ReleaseDate,
                        actorId = m.ActorId,
                        filmTypeId = m.FilmTypeId
                    }).ToList();
                if (movies.Count > 0)
                {
                    return StatusCode(200, movies);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("feladat13")]
        public IActionResult AddMovie([FromBody] AddMovieDto movie,[FromQuery] string uid)
        {
            try
            {
                var storedUid = _configuration["UID"];

                if (uid != storedUid)
                {
                    return Unauthorized("Nincs jogosultsága új film felvételéhez!");
                }

                using var context = new CinemadbContext();

                var newMovie = new Movie
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    ActorId = movie.ActorId,
                    FilmTypeId = movie.FilmTypeId
                };

                context.Movies.Add(newMovie);
                context.SaveChanges();

                return StatusCode(201, new
                {
                    message = "Film hozzáadása sikeresen megtörtént."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
