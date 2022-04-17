using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Response;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.AuthenticateControllerTest
{
    public class RegisterTest
    {
        private readonly Mock<IAuthenticateService> _authService = new Mock<IAuthenticateService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Register_ShouldReturn200Status()
        {
            /// Arrange
            _authService.Setup(_ => _.RegisterAsyncService(It.IsAny<Register>())).ReturnsAsync(AuthenticateMockData.Registered());
            AuthenticateController authController = new AuthenticateController(_authService.Object);
            /// Act
            var result = (OkObjectResult)await authController.Register(AuthenticateMockData.Register());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Register_ShouldReturn400Status()
        {
            /// Arrange
            AuthResponse<bool>? response = null;
            _authService.Setup(_ => _.RegisterAsyncService(It.IsAny<Register>())).ReturnsAsync(response);
            AuthenticateController authController = new AuthenticateController(_authService.Object);
            /// Act
            var result = (BadRequestObjectResult)await authController.Register(AuthenticateMockData.Register());
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 500
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Register_ShouldReturn500Status()
        {
            /// Arrange
            _authService.Setup(_ => _.RegisterAsyncService(It.IsAny<Register>())).ReturnsAsync(AuthenticateMockData.NotRegistered());
            AuthenticateController authController = new AuthenticateController(_authService.Object);
            /// Act
            var result = (ObjectResult)await authController.Register(AuthenticateMockData.Register());
            /// Assert
            result.StatusCode.Should().Be(500);
        }
    }
}
