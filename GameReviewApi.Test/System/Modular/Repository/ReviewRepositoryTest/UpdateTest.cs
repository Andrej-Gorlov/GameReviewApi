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

namespace GameReviewApi.Test.System.Modular.Repository.ReviewRepositoryTest
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
            ReviewDto entity = new()
            {
                ReviewId=1,
                GameId=3,
                Grade= 95,
                ShortStory= "Музыкальное сопровождение тоже на высоте. С восточными нотками, так же атмосферно."
            };
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await reviewRep.Update(entity);
            /// Assert
            Assert.IsType<ReviewDto>(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает правильный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ReturnsRight()
        {
            /// Arrange
            ReviewDto entity = new()
            {
                ReviewId = 1,
                GameId = 3,
                Grade = 95,
                ShortStory = "Музыкальное сопровождение тоже на высоте. С восточными нотками, так же атмосферно."
            };
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            /// Act
            var result = await reviewRep.Update(entity);
            /// Assert
            Assert.Equal(result.ReviewId, entity.ReviewId);
            Assert.Equal(result.GameId, entity.GameId);
            Assert.Equal(result.Grade, entity.Grade);
            Assert.Equal(result.ShortStory, entity.ShortStory);
        }
        /// <summary>
        /// Если из БД вернулся null, проверяем на исключение
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Update_ExceptionReturns()
        {
            /// Arrange
            ReviewDto entity = new()
            {
                ReviewId = ReviewMockData.Get().Count()+1,
                GameId = 3,
                Grade = 95,
                ShortStory = "Музыкальное сопровождение тоже на высоте. С восточными нотками, так же атмосферно."
            };
            ReviewRepository reviewRep = new ReviewRepository(_context, _mapper);
            /// Assert
            await Assert.ThrowsAsync<NullReferenceException>(()=>
                /// Act
                reviewRep.Update(entity));
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
