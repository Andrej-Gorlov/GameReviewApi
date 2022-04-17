using GameReviewApi.Domain.Entity.Dto;
using System.Collections.Generic;
using System.Linq;

namespace GameReviewApi.Test.MockData
{
    public class GenreMockData
    {
        public static GenreDto Entity() =>
             new GenreDto()
             {
                 GenreId = 8,
                 GameId = 3,
                 GenreName = "Slasher"
             };

        public static IEnumerable<GenreDto> Get() =>
            new List<GenreDto>()
            {
                new GenreDto
                {
                    GenreId=1,
                    GameId = 1,
                    GenreName = "Action"
                }, new GenreDto
                {
                    GenreId = 2,
                    GameId = 1,
                    GenreName = "RPG"
                }, new GenreDto
                {
                    GenreId = 3,
                    GameId = 2,
                    GenreName = "Action"
                },new GenreDto
                {
                    GenreId = 4,
                    GameId = 2,
                    GenreName = "Horror"
                },new GenreDto
                {
                    GenreId = 5,
                    GameId = 2,
                    GenreName = "RPG"
                }, new GenreDto
                {
                    GenreId = 6,
                    GameId = 3,
                    GenreName = "Action"
                }, new GenreDto
                {
                    GenreId = 7,
                    GameId = 3,
                    GenreName = "Slasher"
                }
            };

        public static GenreDto? GetById(int id)
        {
            IEnumerable<GenreDto> reservations = Get();
            return reservations.Where(a => a.GenreId == id).FirstOrDefault();
        }

        public static bool Delete(int id)
        {
            IEnumerable<GenreDto> reservations = Get();
            var user = reservations.Where(a => a.GenreId == id).FirstOrDefault();
            if (user is null)
                return false;
            return true;
        }
    }
}
