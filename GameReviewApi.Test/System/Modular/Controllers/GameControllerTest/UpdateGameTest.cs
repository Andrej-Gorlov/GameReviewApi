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
    public class UpdateGameTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateGame_ShouldReturn200Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.UpdateAsyncService(It.IsAny<GameDto>())).ReturnsAsync(GameMockData.Entity());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController.UpdateGame(GameMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateGame_ShouldReturn404Status()
        {
            /// Arrange
            GameDto? gameDto = null;
            _gameService.Setup(_ => _.UpdateAsyncService(It.IsAny<GameDto>())).ReturnsAsync(gameDto);
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NotFoundObjectResult)await gameController.UpdateGame(GameMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
