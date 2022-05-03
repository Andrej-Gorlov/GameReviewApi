using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;

namespace GameReviewApi.Service.Interfaces
{
    public interface IGameService : IBaseService<GameDto>
    {
        Task<PagedList<GameAvgGrade>> GamesAvgGradeAsyncService(GameParameters ownerParameters);
        Task<ReviewsByGam> GameStoriesAndGradesAsyncService(string nameGame);
        Task<PagedList<string>> GamesByGenreAsyncService(string genre, GameParameters gameParameters);
    }
}
