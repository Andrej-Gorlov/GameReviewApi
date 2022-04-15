using System.ComponentModel.DataAnnotations;

namespace GameReviewApi.Domain.Entity.Authenticate
{
    public class Register
    {
        [Required (ErrorMessage = "Введите имя пользователя.")]
        public string? UserName { get; set; }

        [Required (ErrorMessage = "Введите пароль.")]
        public string? Password { get; set; }

        [EmailAddress, Required (ErrorMessage = "Введите электронную почту.")]
        public string? Email { get; set; }
    }
}
