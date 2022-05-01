using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.GameRepositoryTest
{
    public class GetTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public GetTest()
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
        public async Task Get_ReturnsRightType()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Get(GameMockData.Get().FirstOrDefault().GameName);
            /// Assert
            Assert.IsType<ReviewsByGam>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldReturnCorrect()
        {
            /// Arrange
            ReviewsByGam byGam = GameMockData.GameStoriesAndGrades(GameMockData.Get().FirstOrDefault().GameName);
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Get(GameMockData.Get().FirstOrDefault().GameName);
            /// Assert
            Assert.NotNull(result);
            Assert.Equal(result.GameName, byGam.GameName);
            Assert.Equal(result.ShortStories.Count(), byGam.ShortStories.Count());
            Assert.Equal(result.Grades.Count(), byGam.Grades.Count());
            Assert.True(result.ShortStories.All(x => x == byGam.ShortStories.FirstOrDefault(stories => stories == x)));
            Assert.True(result.Grades.All(x => x == byGam.Grades.FirstOrDefault(grade => grade == x)));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
