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

namespace GameReviewApi.Test.System.Modular.Repository.GenreRepositoryTest
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
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.GetById(GenreMockData.Get().FirstOrDefault().GenreId);
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
            invalidId += GenreMockData.Get().LastOrDefault().GenreId;
            Genre genre = _mapper.Map<Genre>(GenreMockData.GetById(validId));
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            //Act
            var nullResult = genreRep.GetById(invalidId);
            var entityResult = genreRep.GetById(genre.GenreId);
            //Assert
            Assert.Equal(genre.GenreId, entityResult.Result.GenreId);
            Assert.Equal(genre.GameId, entityResult.Result.GameId);
            Assert.Equal(genre.GenreName, entityResult.Result.GenreName);
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
            GenreRepository genreRep = new GenreRepository(_context, _mapper);
            /// Act
            var result = await genreRep.GetById(GenreMockData.Get().FirstOrDefault().GenreId);
            /// Assert
            Assert.IsType<GenreDto>(result);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
