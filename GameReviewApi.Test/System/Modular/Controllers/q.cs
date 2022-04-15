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

namespace GameReviewApi.Test.System.Modular.Controllers
{
    public class q
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
            //var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (OkObjectResult)await _userController.GetUsers();
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdUser_ShouldReturn400Status()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = (BadRequestObjectResult)await _userController.GetByIdUser(0);
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
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            var user = UserMockData.Get().Last();
            /// Act
            var result = (NotFoundObjectResult)await _userController.GetByIdUser(user.UserId + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //[Fact]
        //public void Test_GET_AReservations_BadRequest()
        //{
        //    // Arrange
        //    int id = 0;
        //    var mockRepo = new Mock<IRepository>();
        //    mockRepo.Setup(repo => repo[It.IsAny<int>()]).Returns<int>((a) => Single(a));
        //    var controller = new ReservationController(mockRepo.Object);

        //    // Act
        //    var result = controller.Get(id);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<Reservation>>(result);
        //    Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //}

        /// <summary>
        /// Проверяем, что из внутреннего каталога возвращается не null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetUsers_NotNullResult()
        {
            /// Arrange
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAsyncService()).ReturnsAsync(UserMockData.Get());
            UserController _userController = new UserController(userService.Object);
            /// Act
            var result = await _userController.GetUsers();
            /// Assert
            Assert.NotNull(result);
        }

    }
}
