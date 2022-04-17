using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;
using System.Collections.Generic;
using System.Linq;

namespace GameReviewApi.Test.MockData
{
    public class UserMockData
    {
        public static IEnumerable<UserDto> Get() =>
            new List<UserDto>()
            {
                new UserDto()
                {
                    UserId=1,
                    UserName="Join",
                    Password="Admin123*",
                    Email="admin@gmail.com",
                    Role=Role.Admin
                },
                new UserDto()
                {
                    UserId=2,
                    UserName="Rali",
                    Password="User123*",
                    Email="user@gmail.com",
                    Role=Role.User
                },
                new UserDto()
                {
                    UserId=3,
                    UserName="Rex",
                    Password="User123*",
                    Email="user@gmail.com",
                    Role=Role.User
                }
            };

        public static UserDto? GetById(int id)
        {
            IEnumerable<UserDto> reservations = Get();
            return reservations.Where(a => a.UserId == id).FirstOrDefault();
        }

        public static bool Delete(int id)
        {
            IEnumerable<UserDto> reservations = Get();
            var user = reservations.Where(a => a.UserId == id).FirstOrDefault();
            if (user is null)
                return false;
            return true;
        }
    }
}
