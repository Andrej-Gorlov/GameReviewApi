using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class GameService: IGameService
    {
        private IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository) => _gameRepository = gameRepository;
        public async Task<GameDto> CreateAsyncService(GameDto entity) => await _gameRepository.Update(entity);
        public async Task<bool> DeleteAsyncService(int id) => await _gameRepository.Delete(id);  
        public async Task<PagedList<string>> GamesByGenreAsyncService(string genre, GameParameters gameParameters) =>
                PagedList<string>.ToPagedList(
                    await _gameRepository.GetGames(genre),
                    gameParameters.PageNumber,
                    gameParameters.PageSize);

        public async Task<PagedList<GameAvgGrade>> GamesAvgGradeAsyncService(GameParameters gameParameters) =>
                PagedList<GameAvgGrade>.ToPagedList(
                    await _gameRepository.GetGamesAvgGrade(),
                    gameParameters.PageNumber,
                    gameParameters.PageSize);

        public async Task<ReviewsByGam> GameStoriesAndGradesAsyncService(string nameGame)  => await _gameRepository.Get(nameGame);
        public async Task<GameDto> GetByIdAsyncService(int id) => await _gameRepository.GetById(id);
        public async Task<GameDto> UpdateAsyncService(GameDto entity) => await _gameRepository.Update(entity);
    }
}
