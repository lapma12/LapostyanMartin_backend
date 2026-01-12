using Lapostyan_Martin_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lapostyan_Martin_backend.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
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
                        fimcTypeId = m.FilmTypeId
                    }).ToList();
                if (movies.Count > 0)
                {
                    return StatusCode(200, movies);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message});
            }
        }
    }
}
