using System.ComponentModel.DataAnnotations;

namespace GameReviewApi.Domain.Entity.Authenticate
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required (ErrorMessage = "Введите имя пользователя.")]
        public string? UserName { get; set; }

        [Required (ErrorMessage = "Введите пароль.")]
        public string? Password { get; set; }

        [EmailAddress, Required (ErrorMessage = "Введите электронную почту.")]
        public string? Email { get; set; }

        public Role Role { get; set; }
    }
}
