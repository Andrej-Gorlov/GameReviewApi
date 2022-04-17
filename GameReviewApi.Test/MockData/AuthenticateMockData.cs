using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Response;

namespace GameReviewApi.Test.MockData
{
    public class AuthenticateMockData
    {
        public static Login Login() =>
             new Login()
             {
                 UserName = "Riley",
                 Password = "Riley123*"
             };

        public static Register Register() =>
             new Register()
             {
                 UserName = "Riley",
                 Password = "Riley123*",
                 Email = "Rily@gmail.com"
             };

        public static AuthResponse<string> Authenticate() =>
             new AuthResponse<string>
             {
                 Result = "Токен: 123456789",
                 StatusCode = 200,
                 Status = "Успех.",
                 Message = "Токен действительн три часа."
             };

        public static AuthResponse<string> NotAuthenticate() =>
             new AuthResponse<string>
             {
                 Result = "Неавторизованный",
                 StatusCode = 401,
                 Status = "Ошибка.",
                 Message = "Пользователь не авторизовался."
             };

        public static AuthResponse<bool> Registered() =>
             new AuthResponse<bool>
             {
                 Result = true,
                 StatusCode = 201,
                 Status = "Успех.",
                 Message = "Токен действительн три часа."
             };

        public static AuthResponse<bool> NotRegistered() =>
             new AuthResponse<bool>
             {
                 Result = false,
                 StatusCode = 500,
                 Status = "Ошибка.",
                 Message = "Пользователь не авторизовался."
             };
    }
}
