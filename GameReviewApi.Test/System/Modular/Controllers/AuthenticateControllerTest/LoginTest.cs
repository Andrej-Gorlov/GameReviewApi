using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.AuthenticateControllerTest
{
    public class LoginTest
    {
        private readonly Mock<IAuthenticateService> _authService = new Mock<IAuthenticateService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Login_ShouldReturn200Status()
        {
            /// Arrange
            _authService.Setup(_ => _.LoginAsyncService(It.IsAny<Login>())).ReturnsAsync(AuthenticateMockData.Authenticate());
            AuthenticateController authController = new AuthenticateController(_authService.Object);
            /// Act
            var result = (OkObjectResult)await authController.Login(AuthenticateMockData.Login());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 401
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Login_ShouldReturn401Status()
        {
            /// Arrange
            _authService.Setup(_ => _.LoginAsyncService(It.IsAny<Login>())).ReturnsAsync(AuthenticateMockData.NotAuthenticate());
            AuthenticateController authController = new AuthenticateController(_authService.Object);
            /// Act
            var result = (UnauthorizedObjectResult)await authController.Login(AuthenticateMockData.Login());
            /// Assert
            result.StatusCode.Should().Be(401);
        }
    }
}
