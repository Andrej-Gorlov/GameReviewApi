using AutoMapper;
using FluentAssertions;
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
    public class CreateTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public CreateTest()
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
        /// Проверяет что обработчик возвращает правильное количество записей в бд 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_RightRecordCountToDb()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Create(GameMockData.Entity());
            /// Assert
            int expectedRecordCount = GameMockData.Get().Count() + 1;
            _context.Game.Count().Should().Be(expectedRecordCount);
        }

        /// <summary>
        /// Обработчик проверяет последнюю сущность в бд
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsLastEntityToDb()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = _mapper.Map<Game>(await gameRep.Create(GameMockData.Entity()));
            var entity = _context.Game.LastOrDefault();
            /// Assert
            Assert.Equal(result.GameName, entity.GameName);
            Assert.Equal(result.GameId, entity.GameId);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsRightType()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Create(GameMockData.Entity());
            /// Assert
            Assert.IsType<GameDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает новый обзор
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsNewReview()
        {
            /// Arrange
            GameDto game = GameMockData.Entity();
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Create(game);
            /// Assert
            Assert.Equal(game.GameName, result.GameName);
            Assert.Equal(game.GameId, result.GameId);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
