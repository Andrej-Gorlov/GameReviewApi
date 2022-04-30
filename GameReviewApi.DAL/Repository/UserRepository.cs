using AutoMapper;
using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApi.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserDto> GetById(int id) =>
           _mapper.Map<UserDto>(await _db.User.FirstOrDefaultAsync(x => x.UserId == id));

        public async Task<bool> Delete(int id)
        {
            try
            {
                User user = await _db.User.FirstOrDefaultAsync(x => x.UserId == id);
                if (user is null) 
                {
                    return false;
                }
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserDto>> Get() =>
            _mapper.Map<List<UserDto>>(await _db.User.ToListAsync());
    }
}
