namespace Lapostyan_Martin_backend.Models.Dtos
{
    public class AddMovieDto
    {
        public int MovieId { get; set; }

        public string Title { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public int ActorId { get; set; }

        public int FilmTypeId { get; set; }

    }
}
