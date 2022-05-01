using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.GameRepositoryTest
{
    public class GetGamesTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public GetGamesTest()
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
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ReturnsRightType()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.GetGames(GenreMockData.Get().LastOrDefault().GenreName);
            /// Assert
            Assert.IsType<List<string>>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ShouldReturnCorrect()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.GetGames(GenreMockData.Get().FirstOrDefault().GenreName);
            /// Assert
            Assert.NotNull(result);
            Assert.Equal(GameMockData.GamesByGenre(
                GenreMockData.Get().FirstOrDefault().GenreName).Count(), result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
