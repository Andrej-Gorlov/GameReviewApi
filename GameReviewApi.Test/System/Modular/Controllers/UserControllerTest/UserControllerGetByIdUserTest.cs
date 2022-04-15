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
    public class UserControllerGetByIdUserTest
    {
        private readonly Mock<IUserService> userService = new Mock<IUserService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_ShouldReturn200Status()
        {
            /// Arrange
            int id = 1;
            userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(id));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (OkObjectResult)await _userController.GetByIdUser(id);
            /// Assert
            result.StatusCode.Should().Be(200);

            //var actionResult = Assert.IsType<OkObjectResult>(result);
            //var actionValue = Assert.IsType<OkObjectResult>(actionResult);
            //Assert.Equal(id, ((UserDto)actionValue.Value).UserId);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0; 
            userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(It.IsAny<int>()));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (BadRequestObjectResult)await _userController.GetByIdUser(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_ShouldReturn404Status()
        {
            /// Arrange
            userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(It.IsAny<int>()));
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (NotFoundObjectResult)await _userController.GetByIdUser(UserMockData.Get().Count()+1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
