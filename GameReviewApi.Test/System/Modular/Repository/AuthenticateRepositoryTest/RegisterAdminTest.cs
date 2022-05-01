using AutoMapper;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Repository.AuthenticateRepositoryTest
{
    public class RegisterAdminTest : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private static IMapper? _mapper;

        public RegisterAdminTest()
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

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>())
                .Build();
        }

        /// <summary>
        /// Проверяет что обработчик возвращает true 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterAdmin_ReturnsTrue()
        {
            /// Arrange
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            AuthenticateRepository authRep = new AuthenticateRepository(_context, _configuration);
            /// Act
            var result = await authRep.RegisterAdmin(AuthenticateMockData.Register());
            /// Assert
            Assert.True(result);
        }
        /// <summary>
        /// Проверяет что обработчик возвращает false 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RegisterAdmin_ReturnsFalse()
        {
            /// Arrange
            _context.User.AddRange(_mapper.Map<List<User>>(UserMockData.Get()));
            _context.SaveChanges();
            User userAdmin = _mapper.Map<User>(UserMockData.Get().FirstOrDefault());
            Register register = new()
            {
                UserName = userAdmin.UserName,
                Password = userAdmin.Password,
                Email = userAdmin.Email
            };
            AuthenticateRepository authRep = new AuthenticateRepository(_context, _configuration);
            /// Act
            var result = await authRep.RegisterAdmin(register);
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
