namespace GameReviewApi.Domain.Entity.Dto
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public string? ShortStory { get; set; }
        public int Grade { get; set; }
    }
}
