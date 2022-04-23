using AutoMapper;
using FluentAssertions;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.ReviewRepositoryTest
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
            ReviewRepository userRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await userRep.Create(ReviewMockData.Entity());
            /// Assert
            int expectedRecordCount = ReviewMockData.Get().Count() + 1;
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
            ReviewRepository userRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await userRep.Create(ReviewMockData.Entity());
            /// Assert
            Assert.Equal(_mapper.Map<Review>(result),_context.Review.LastOrDefault());
        }

        /// <summary>
        /// Проверяет что обработчик возвращает правильный тип 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsRightType()
        {
            /// Arrange
            ReviewRepository userRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await userRep.Create(ReviewMockData.Entity());
            /// Assert
            Assert.IsType<ReviewDto>(result);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает новый обзор
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_ReturnsNewReview()
        {
            /// Arrange
            ReviewDto review = ReviewMockData.Entity();
            ReviewRepository userRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await userRep.Create(review);
            /// Assert
            Assert.Equal(review.ReviewId, result.ReviewId);
            Assert.Equal(review.GameId, result.GameId);
            Assert.Equal(review.Grade, result.Grade);
            Assert.Equal(review.ShortStory, result.ShortStory);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
