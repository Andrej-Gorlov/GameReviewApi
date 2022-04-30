using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.ReviewRepositoryTest
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
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await reviewRep.GetById(ReviewMockData.Get().FirstOrDefault().ReviewId);
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
            invalidId += ReviewMockData.Get().LastOrDefault().ReviewId;
            Review review = _mapper.Map<Review>(ReviewMockData.GetById(validId));
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            //Act
            var nullResult = reviewRep.GetById(invalidId);
            var entityResult = reviewRep.GetById(review.ReviewId);
            //Assert
            Assert.Equal(review.ReviewId, entityResult.Result.ReviewId);
            Assert.Equal(review.GameId, entityResult.Result.GameId);
            Assert.Equal(review.Grade, entityResult.Result.Grade);
            Assert.Equal(review.ShortStory, entityResult.Result.ShortStory);
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
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await reviewRep.GetById(ReviewMockData.Get().FirstOrDefault().ReviewId);
            /// Assert
            Assert.IsType<ReviewDto>(result);
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
