using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity;
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
    public class GetGamesAvgGradeTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public GetGamesAvgGradeTest()
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
        public async Task GetGamesAvgGrade_ReturnsRightType()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.GetGamesAvgGrade();
            /// Assert
            Assert.IsType<List<GameAvgGrade>>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGamesAvgGrade_ShouldReturnCorrect()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            // Act
            var result = await gameRep.GetGamesAvgGrade();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(GameMockData.GameAvgGrade().Count(), result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
