using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.UserControllerTest
{
    public class UserControllerGetUsersTest
    {
        private readonly Mock<IUserService> userService = new Mock<IUserService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_ShouldReturn200Status()
        {
            /// Arrange
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (OkObjectResult)await _userController.GetUsers();
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
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            // Act
            var result = await _userController.GetUsers();
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserDto>>(viewResult.Value);
            Assert.Equal(UserMockData.Get().Count(), model.Count());
        }
    }
}
