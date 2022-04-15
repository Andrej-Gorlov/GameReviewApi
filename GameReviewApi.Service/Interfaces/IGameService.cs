using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi.Service.Interfaces
{
    public interface IGameService : IBaseService<GameDto>
    {
        Task<List<GameAvgGrade>> GamesAvgGradeAsyncService();
        Task<ReviewsByGam> GameStoriesAndGradesAsyncService(string nameGame);
        Task<IEnumerable<string>> GamesByGenreAsyncService(string genre);
    }
}
