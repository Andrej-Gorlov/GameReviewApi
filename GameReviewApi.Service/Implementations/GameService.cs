using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class GameService: IGameService
    {
        private IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository) => _gameRepository = gameRepository;
        public async Task<GameDto> CreateAsyncService(GameDto entity) => await _gameRepository.Update(entity);
        public async Task<bool> DeleteAsyncService(int id) => await _gameRepository.Delete(id);  
        public async Task<IEnumerable<string>> GamesByGenreAsyncService(string genre) => await _gameRepository.GetGames(genre);
        public async Task<List<GameAvgGrade>> GamesAvgGradeAsyncService() => await _gameRepository.GetGamesAvgGrade();
        public async Task<ReviewsByGam> GameStoriesAndGradesAsyncService(string nameGame)  => await _gameRepository.Get(nameGame);
        public async Task<GameDto> GetByIdAsyncService(int id) => await _gameRepository.GetById(id);
        public async Task<GameDto> UpdateAsyncService(GameDto entity) => await _gameRepository.Update(entity);
    }
}
