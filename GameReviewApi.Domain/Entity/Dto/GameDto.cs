namespace GameReviewApi.Domain.Entity.Dto
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public List<ReviewDto>? Reviews { get; set; }
        public List<GenreDto>? Genres { get; set; }
    }
}
