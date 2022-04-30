using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.GameRepositoryTest
{
    public class DeleteTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;

        public DeleteTest()
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
        /// Проверяет что обработчик возвращает true 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ReturnsTrue()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Delete(GameMockData.Get().FirstOrDefault().GameId);
            /// Assert
            Assert.True(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает false 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Delete_ReturnsFalse()
        {
            /// Arrange
            GameRepository gameRep = new GameRepository(_context, _mapper);
            /// Act
            var result = await gameRep.Delete(GameMockData.Get().Count() + 1);
            /// Assert
            Assert.False(result);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
