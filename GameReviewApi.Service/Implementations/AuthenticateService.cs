using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Response;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class AuthenticateService : IAuthenticateService
    {
        private IAuthenticateRepository _authRepository;
        public AuthenticateService(IAuthenticateRepository authRepository) => _authRepository = authRepository;
        public async Task<AuthResponse<string>> LoginAsyncService(Login model)
        {
            string result = await _authRepository.Login(model);
            if (result == "401")
            {
                return new AuthResponse<string>() { Result = "Неавторизованный", StatusCode = 401, Status = "Ошибка.", Message = "Пользователь не авторизовался." };
            }
            else
            {
                return new AuthResponse<string>() { Result = $"Токен: {result}", StatusCode = 200, Status = "Успех.", Message = "Токен действительн три часа." };
            }
        }
        public async Task<AuthResponse<bool>> RegisterAsyncService(Register model)
        {
            bool result = await _authRepository.Register(model);
            if (!result) 
            {
                return new AuthResponse<bool>() { Result = false, StatusCode = 500, Status = "Ошибка.", Message = "Пользователь уже существует!" };
            }
            else
            {
                return new AuthResponse<bool>() { Result = true, Status = "Успех", StatusCode = 201, Message = "Пользователь создан!" };
            }        
        }
        public async Task<AuthResponse<bool>> RegisterAdminAsyncService(Register model)
        {
            bool result = await _authRepository.RegisterAdmin(model);
            if (!result)
            {
                return new AuthResponse<bool>() { Result = false, StatusCode = 500, Status = "Ошибка.", Message = "Пользователь [Admin] уже существует!" };
            }
            else
            {
                return new AuthResponse<bool>() { Result = true, Status = "Успех", StatusCode = 201, Message = "Пользователь [Admin] создан!" };
            }
        }
    }
}
