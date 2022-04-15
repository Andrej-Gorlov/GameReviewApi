using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsyncService(int id);
        Task<bool> DeleteAsyncService(int id);
        Task<IEnumerable<UserDto>> GetAsyncService();
    }
}
