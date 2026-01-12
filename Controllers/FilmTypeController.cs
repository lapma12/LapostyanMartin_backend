using Lapostyan_Martin_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lapostyan_Martin_backend.Controllers
{
    [Route("api/filmtypes")]
    [ApiController]
    public class FilmTypeController : ControllerBase
    {
        [HttpGet("feladat11")]
        public IActionResult GetAllFilmTypes()
        {
            try
            {
                using var context = new CinemadbContext();
                var filmTypes = context.FilmTypes
                    .Select(f => new
                    {
                        typeId = f.TypeId,
                        typeName = f.TypeName,
                        Movie = context.Movies
                            .Where(m => m.FilmTypeId == f.TypeId)
                            .Select(m => new
                            {
                                movieId = m.MovieId,
                                title = m.Title,
                                releaseDate = m.ReleaseDate,
                                actorId = m.ActorId,
                                filmTypeId = m.FilmTypeId
                            }).ToList()
                    }).ToList();
                if (filmTypes.Count > 0)
                {
                    return StatusCode(200, filmTypes);
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
