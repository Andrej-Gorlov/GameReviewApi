namespace GameReviewApi.Domain.Entity.Dto
{
    public class GenreDto
    {
        public int GenreId { get; set; }
        public int GameId { get; set; }
        public string? GenreName { get; set; }
    }
}
