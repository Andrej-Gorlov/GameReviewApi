using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<bool> DeleteAsyncService(int id) => await _userRepository.Delete(id);
        public async Task<PagedList<UserDto>> GetAsyncService(UserParameters userParameters) =>
            PagedList<UserDto>.ToPagedList(await _userRepository.Get()
                , userParameters.PageNumber
                , userParameters.PageSize);

        public async Task<UserDto> GetByIdAsyncService(int id)=>await _userRepository.GetById(id);
    }
}
