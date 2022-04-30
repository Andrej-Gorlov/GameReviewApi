using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.GenreRepositoryTest
{
    public class UpdateTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public UpdateTest()
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
        public async Task Update_ReturnsRightType()
        {
            /// Arrange
            GenreDto entity = new()
            {
                GenreId = 7,
                GameId = 3,
                GenreName = "RPG"
            };
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.Update(entity);
            /// Assert
            Assert.IsType<GenreDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ReturnsRight()
        {
            /// Arrange
            GenreDto entity = new()
            {
                GenreId = 7,
                GameId = 3,
                GenreName = "RPG"
            };
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.Update(entity);
            /// Assert
            Assert.Equal(result.GenreId, entity.GenreId);
            Assert.Equal(result.GameId, entity.GameId);
            Assert.Equal(result.GenreName, entity.GenreName);
        }
        /// <summary>
        /// Если из БД вернулся null, проверяем на исключение
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ExceptionReturns()
        {
            /// Arrange
            GenreDto entity = new()
            {
                GenreId = GenreMockData.Get().Count()+1,
                GameId = 3,
                GenreName = "RPG"
            };
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Assert
            await Assert.ThrowsAsync<NullReferenceException>(() =>
                /// Act
                genreRep.Update(entity));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
