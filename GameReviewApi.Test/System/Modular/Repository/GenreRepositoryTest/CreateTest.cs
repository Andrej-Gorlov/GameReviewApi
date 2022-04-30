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

namespace GameReviewApi.Test.System.Modular.Repository.GenreRepositoryTest
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
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.Create(GenreMockData.Entity());
            /// Assert
            int expectedRecordCount = GenreMockData.Get().Count() + 1;
            _context.Review.Count().Should().Be(expectedRecordCount);
        }

        /// <summary>
        /// Обработчик проверяет последнюю сущность в бд
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsLastEntityToDb()
        {
            /// Arrange
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = _mapper.Map<Genre>(await genreRep.Create(GenreMockData.Entity()));
            var entity = _context.Genre.LastOrDefault();
            /// Assert
            Assert.Equal(result.GenreId, entity.GenreId);
            Assert.Equal(result.GameId, entity.GameId);
            Assert.Equal(result.GenreName, entity.GenreName);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsRightType()
        {
            /// Arrange
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.Create(GenreMockData.Entity());
            /// Assert
            Assert.IsType<GenreDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает новый обзор
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsNewReview()
        {
            /// Arrange
            GenreDto genre = GenreMockData.Entity();
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.Create(genre);
            /// Assert
            Assert.Equal(genre.GenreId, result.GenreId);
            Assert.Equal(genre.GameId, result.GameId);
            Assert.Equal(genre.GenreName, result.GenreName);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
