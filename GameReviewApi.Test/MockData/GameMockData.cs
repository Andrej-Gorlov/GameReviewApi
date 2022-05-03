using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewApi.Test.MockData
{
    public class GameMockData
    {
        private static int pageNumber = 1;
        private static int pageSize = Get().Count();

        public static GameDto Entity() =>
             new GameDto()
             {
                 GameId = 4,
                 GameName = "Empire Total War"
             };

        public static List<GameDto> Get() =>
            new List<GameDto>
            {
                new GameDto
                {
                    GameId = 1,
                    GameName = "Assassin's Creed"
                }, new GameDto
                {
                    GameId = 2,
                    GameName = "Resident Evil"
                }, new GameDto
                {
                    GameId = 3,
                    GameName = "Devil May Cry"
                }
            };

        public static GameDto? GetById(int id)
        {
            IEnumerable<GameDto> reservations = Get();
            return reservations.Where(a => a.GameId == id).FirstOrDefault();
        }

        public static bool Delete(int id)
        {
            IEnumerable<GameDto> reservations = Get();
            var user = reservations.Where(a => a.GameId == id).FirstOrDefault();
            if (user is null)
                return false;
            return true;
        }

        public static PagedList<GameAvgGrade> GameAvgGrade() =>
            PagedList<GameAvgGrade>.ToPagedList(Get().Select(x =>new GameAvgGrade
            {
                GameName = x.GameName,
                Grade =(int)ReviewMockData.Get().Where(game => game.GameId == x.GameId).Average(avg => avg.Grade)
            }).OrderByDescending(sort => sort.Grade).ToList(), pageNumber, pageSize);
        
        public static ReviewsByGam GameStoriesAndGrades(string nameGame) =>
            new()
            {
                GameName = nameGame,
                ShortStories = ReviewMockData.Get().Where(x => x.GameId == GameId(nameGame)).Select(x => x.ShortStory).ToList(),
                Grades = ReviewMockData.Get().Where(x => x.GameId == GameId(nameGame)).Select(x => x.Grade).ToList()
            };

        private static int GameId(string nameGame) =>
            Get().FirstOrDefault(x => x.GameName.ToUpper().Replace(" ", "") == nameGame.ToUpper().Replace(" ", "")).GameId;

        public static PagedList<string> GamesByGenre(string genre) =>
            PagedList<string>.ToPagedList(Get().Where(game => GenreMockData.Get().Where(id=>id.GameId==game.GameId)
                .Any(g => g.GenreName.ToUpper().Replace(" ", "") == genre.ToUpper().Replace(" ", "")))
                .Select(x => x.GameName).ToList(), pageNumber, pageSize);
    }
}
