using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;

namespace GameReviewApi.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsyncService(int id);
        Task<bool> DeleteAsyncService(int id);
        Task<PagedList<UserDto>> GetAsyncService(UserParameters userParameters);
    }
}
