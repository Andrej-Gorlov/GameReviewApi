using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi.DAL.Interfaces
{
    public interface IUserRepository 
    {
        Task<UserDto> GetById(int id);
        Task<bool> Delete(int id);
        Task<IEnumerable<UserDto>> Get();
    }
}
