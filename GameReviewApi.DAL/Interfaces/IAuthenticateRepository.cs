using GameReviewApi.Domain.Entity.Authenticate;

namespace GameReviewApi.DAL.Interfaces
{
    public interface IAuthenticateRepository 
    {
        Task<bool> Register(Register entity);
        Task<bool> RegisterAdmin(Register entity);
        Task<string> Login(Login entity);
    }
}
