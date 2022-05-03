using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Paging;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.UserControllerTest
{
    public class GetUsersTest
    {
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_ShouldReturn200Status()
        {
            /// Arrange
            int pageNumber = 1;
            int pageSize = UserMockData.Get().Count();
            UserParameters userParameters = new();
            PagedList<UserDto> users = PagedList<UserDto>.ToPagedList(UserMockData.Get(), pageNumber, pageSize);
            _userService.Setup(_ => _.GetAsyncService(userParameters)).ReturnsAsync(users);
            UserController userController = new UserController(_userService.Object);
            /// Act
            var result = (OkObjectResult)await userController.GetUsers(userParameters);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_ShouldReturnCorrect()
        {
            /// Arrange
            int pageNumber = 1;
            int pageSize = UserMockData.Get().Count();
            UserParameters userParameters = new();
            PagedList<UserDto> users = PagedList<UserDto>.ToPagedList(UserMockData.Get(), pageNumber, pageSize);
            _userService.Setup(_ => _.GetAsyncService(userParameters)).ReturnsAsync(users);
            UserController userController = new UserController(_userService.Object);
            // Act
            var result = await userController.GetUsers(userParameters);
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PagedList<UserDto>>(viewResult.Value);
            Assert.Equal(UserMockData.Get().Count(), model.Count());
        }
    }
}
