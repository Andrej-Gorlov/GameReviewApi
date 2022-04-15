namespace GameReviewApi.Domain.Entity.Dto
{
    public class GameDto
    {
        public int GameId { get; set; }
        public string? GameName { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
