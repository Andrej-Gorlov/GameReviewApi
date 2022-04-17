using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GameControllerTest
{
    public class CreateGameTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 201
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateGame_ShouldReturn201Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.CreateAsyncService(It.IsAny<GameDto>())).ReturnsAsync(GameMockData.Entity());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (CreatedAtActionResult)await gameController.CreateGame(GameMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(201);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateGame_ShouldReturn400Status()
        {
            /// Arrange
            GameDto? gameDto = null;
            _gameService.Setup(_ => _.CreateAsyncService(It.IsAny<GameDto>())).ReturnsAsync(gameDto);
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (BadRequestObjectResult)await gameController.CreateGame(gameDto);
            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
