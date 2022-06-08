using AutoMapper;
using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApi.DAL.Repository
{
    public class GameRepository: IGameRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public GameRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GameDto> Create(GameDto entity)
        {
            Game game = _mapper.Map<GameDto, Game>(entity);
            _db.Game.Add(game);
            await _db.SaveChangesAsync();
            return _mapper.Map<Game, GameDto>(game);
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Game game = await _db.Game.Include(x => x.Genres).Include(x => x.Reviews).FirstOrDefaultAsync(x => x.GameId == id);
                if (game == null) 
                {
                    return false;
                }
                _db.Game.Remove(game);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ReviewsByGam> Get(string nameGame) => 
            new()
            {
                GameName = nameGame,
                ShortStories = _db.Review.Where(x => x.GameId == GameId(nameGame)).Select(x => x.ShortStory).ToList(),
                Grades = await _db.Review.Where(x => x.GameId == GameId(nameGame)).Select(x => x.Grade).ToListAsync()
            };
        private int GameId(string nameGame)
        {
            Game game = _db.Game.FirstOrDefault(x => x.GameName.ToUpper().Replace(" ", "") == nameGame.ToUpper().Replace(" ", ""));
            return game.GameId;
        }
        public async Task<GameDto> GetById(int id)
        {
            Game game = await _db.Game.Include(s => s.Genres).Include(s => s.Reviews).FirstOrDefaultAsync(x => x.GameId == id);
            return _mapper.Map<GameDto>(game);
        }
        public async Task<IEnumerable<string>> GetGames(string genre) =>
            _mapper.Map<List<string>>(await _db.Game.Where(game => game.Genres
                .Any(g => g.GenreName.ToUpper().Replace(" ", "") == genre.ToUpper().Replace(" ", "")))
                .Select(x => x.GameName).ToListAsync());
        public async Task<List<GameAvgGrade>> GetGamesAvgGrade() =>
            await _db.Game.Include(x => x.Reviews).Select(x => new GameAvgGrade
            {
                GameName = x.GameName,
                Grade = (int)x.Reviews.Where(game => game.GameId == x.GameId).Average(avg => avg.Grade)
            }).OrderByDescending(sort => sort.Grade).ToListAsync();
        
        public async Task<GameDto> Update(GameDto entity)
        {
            Game game = _mapper.Map<GameDto, Game>(entity);
            if (await _db.Game.AsNoTracking().FirstOrDefaultAsync(x => x.GameId == entity.GameId) is null)
            {
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            _db.Game.Update(game);
            await _db.SaveChangesAsync();
            return _mapper.Map<Game, GameDto>(game);
        }
    }
}
