using FluentAssertions;
using GameReviewApi.Controllers;
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
    public class UserControllerDeleteUserTest
    {
        private readonly Mock<IUserService> userService = new Mock<IUserService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 204
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUser_ShouldReturn204Status()
        {
            /// Arrange
            userService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.Delete(UserMockData.Get().FirstOrDefault().UserId));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (NoContentResult)await _userController.DeleteUser(UserMockData.Get().FirstOrDefault().UserId);
            /// Assert
            result.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUser_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            userService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.Delete(It.IsAny<int>()));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (BadRequestObjectResult)await _userController.DeleteUser(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteUser_ShouldReturn404Status()
        {
            /// Arrange
            userService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.Delete(UserMockData.Get().Count() + 1));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (NotFoundObjectResult)await _userController.DeleteUser(UserMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
