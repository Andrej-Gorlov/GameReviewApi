using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.GameRepositoryTest
{
    public class GetByIdTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;

        public GetByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();

            if (_mapper is null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new SourceMappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        /// <summary>
        /// Проверяет что обработчик возвращает не null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_NotNullResult()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.GetById(GameMockData.Get().FirstOrDefault().GameId);
            /// Assert
            Assert.NotNull(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <param name="validId"></param>
        /// <param name="invalidId"></param>
        [Theory]
        [InlineData(1, 1)]
        public void GetById_ReturnsRight(int validId, int invalidId)
        {
            //Arrange
            invalidId += GameMockData.Get().LastOrDefault().GameId;
            Game game = _mapper.Map<Game>(GameMockData.GetById(validId));
            GameRepository gameRep = new GameRepository(_context, _mapper);
            //Act
            var nullResult = gameRep.GetById(invalidId);
            var entityResult = gameRep.GetById(game.GameId);
            //Assert
            Assert.Equal(game.GameName, entityResult.Result.GameName);
            Assert.Equal(game.GameId, entityResult.Result.GameId);
            Assert.Equal(null, nullResult.Result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetById_ReturnsRightType()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.GetById(GameMockData.Get().FirstOrDefault().GameId);
            /// Assert
            Assert.IsType<GameDto>(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
