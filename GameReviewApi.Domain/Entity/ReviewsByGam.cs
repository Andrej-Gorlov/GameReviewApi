namespace GameReviewApi.Domain.Entity
{
    public class ReviewsByGam
    {
        public string? GameName { get; set; }
        public List<string>? ShortStories { get; set; }
        public List<int>? Grades { get; set; }
    }
}
