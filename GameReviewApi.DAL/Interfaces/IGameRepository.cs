using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi.DAL.Interfaces
{
    public interface IGameRepository: IBaseRepository<GameDto>
    {
        Task<List<GameAvgGrade>> GetGamesAvgGrade();
        Task<ReviewsByGam> Get(string nameGame);
        Task<IEnumerable<string>> GetGames(string genre);
    }
}
