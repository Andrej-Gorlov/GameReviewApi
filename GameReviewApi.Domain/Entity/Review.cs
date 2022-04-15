using System.ComponentModel.DataAnnotations;

namespace GameReviewApi.Domain.Entity
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set;}

        [Required (ErrorMessage = "Укажите id игры.")]
        public int GameId { get; set;}

        [Required (ErrorMessage = "Введите краткий рассказ на игру.")]
        public string? ShortStory { get; set;}

        [Range (1, 100, ErrorMessage = "Недопустимая оценка (диапазон: 1 - 100)."), Required (ErrorMessage = "Укажите оценку игре.")]
        public int Grade { get; set;}
    }
}
