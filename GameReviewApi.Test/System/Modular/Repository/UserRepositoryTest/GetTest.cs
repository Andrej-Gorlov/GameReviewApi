using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
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

namespace GameReviewApi.Test.System.Modular.Repository.UserRepositoryTest
{
    public class GetTest: IDisposable
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

            if (_mapper == null)
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
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ShouldReturnCorrect()
        {
            /// Arrange
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            UserRepository userRep = new UserRepository(_context, _mapper);
            // Act
            var result = await userRep.Get();
            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDto>(result.First());
            Assert.Equal(UserMockData.Get().Count(), result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
