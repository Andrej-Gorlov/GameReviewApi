using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.UserControllerTest
{
    public class GetByIdUserTest
    {
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_ShouldReturn200Status()
        {
            /// Arrange
            int id = 1;
            _userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(id));
            UserController userController = new UserController(_userService.Object);
            /// Act
            var result = (OkObjectResult)await userController.GetByIdUser(id);
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
            _userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(It.IsAny<int>()));
            UserController userController = new UserController(_userService.Object);
            /// Act
            var result = (BadRequestObjectResult)await userController.GetByIdUser(id);
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
            _userService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(UserMockData.GetById(It.IsAny<int>()));
            UserController userController = new UserController(_userService.Object);
            /// Act
            var result = (NotFoundObjectResult)await userController.GetByIdUser(UserMockData.Get().Count()+1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
