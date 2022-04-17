using AutoFixture;
using AutoMapper;
using FluentAssertions;
using GameReviewApi.DAL;
using GameReviewApi.DAL.Repository;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Implementations;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.Helpers;
using GameReviewApi.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Repository
{
    public class UserRepositoryTest /*: IDisposable*/
    {
        protected readonly ApplicationDbContext _context;
        private static IMapper? _mapper;
        public UserRepositoryTest()
        {
            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //        .Options;

            //_context = new ApplicationDbContext(options);
            //_context.Database.EnsureCreated();

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
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_NotNullResult()
        {
            /// Arrange
            //_context.User?.AddRange(UserMockData.GetUsersq());
            //_context.SaveChanges();
            var userRep = new UserRepository(_context, _mapper);
            /// Act
            var result = await userRep.GetById(1);
            /// Assert
            Assert.NotNull(result);
        }
        //public void Dispose()
        //{
        //    _context.Database.EnsureDeleted();
        //    _context.Dispose();
        //}
    }
}
