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
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            UserRepository userRep = new UserRepository(_context, _mapper);
            /// Act
            var result = await userRep.GetById(UserMockData.Get().FirstOrDefault().UserId);
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
            invalidId += UserMockData.Get().LastOrDefault().UserId;
            User user = _mapper.Map<User>(UserMockData.GetById(validId));
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            UserRepository userRep = new UserRepository(_context, _mapper);
            //Act
            var nullResult = userRep.GetById(invalidId);
            var entityResult = userRep.GetById(user.UserId);
            //Assert
            Assert.Equal(user.UserId, entityResult.Result.UserId);
            Assert.Equal(user.UserName, entityResult.Result.UserName);
            Assert.Equal(user.Password, entityResult.Result.Password);
            Assert.Equal(user.Email, entityResult.Result.Email);
            Assert.Equal(user.Role, entityResult.Result.Role);
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
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            UserRepository userRep = new UserRepository(_context, _mapper);
            /// Act
            var result = await userRep.GetById(UserMockData.Get().FirstOrDefault().UserId);
            /// Assert
            Assert.IsType<UserDto>(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
