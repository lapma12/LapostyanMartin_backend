using Lapostyan_Martin_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lapostyan_Martin_backend.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        [HttpGet("feladat9/{name}")]
        public IActionResult GetActorAllDataByName(string name)
        {
            try
            {
                using var context = new CinemadbContext();
                var actor = context.Actors
                    .Include(a => a.Movies)
                    .Where(a => a.ActorName == name)
                    .Select(a => new
                    {
                        actorId = a.ActorId,
                        actorName = a.ActorName,
                        Movies = a.Movies.Select(m => new
                        {
                            movieId = m.MovieId,
                            movieTitle = m.Title,
                            releaseDate = m.ReleaseDate,
                            actorId = m.ActorId,
                            filmTypeId = m.FilmTypeId,
                        }).ToList()
                    })
                    .FirstOrDefault();
                if(actor != null)
                {
                    return StatusCode(200, actor);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
