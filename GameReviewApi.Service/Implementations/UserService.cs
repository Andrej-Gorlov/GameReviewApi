using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<bool> DeleteAsyncService(int id) => await _userRepository.Delete(id);
        public async Task<IEnumerable<UserDto>> GetAsyncService() => await _userRepository.Get();
        public async Task<UserDto> GetByIdAsyncService(int id)=>await _userRepository.GetById(id);
    }
}
