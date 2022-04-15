using System.ComponentModel.DataAnnotations;

namespace GameReviewApi.Domain.Entity
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required (ErrorMessage = "Укажите название игры")]
        public string? GameName { get; set; }

        public List<Review>? Reviews { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}
