using System.ComponentModel.DataAnnotations;

namespace GameReviewApi.Domain.Entity
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required (ErrorMessage = "Укажите id игры")]
        public int GameId { get; set; }

        [Required (ErrorMessage = "Укажите название жанра")]
        public string? GenreName { get; set; }
    }
}
