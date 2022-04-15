using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Response;

namespace GameReviewApi.Service.Interfaces
{
    public interface IAuthenticateService
    {
        Task<AuthResponse<bool>> RegisterAsyncService(Register model);
        Task<AuthResponse<bool>> RegisterAdminAsyncService(Register model);
        Task<AuthResponse<string>> LoginAsyncService(Login model);
    }
}
