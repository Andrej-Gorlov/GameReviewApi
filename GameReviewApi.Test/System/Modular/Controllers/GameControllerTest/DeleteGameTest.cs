using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GameControllerTest
{
    public class DeleteGameTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 204
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGame_ShouldReturn204Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>()))
                .ReturnsAsync(GameMockData.Delete(GameMockData.Get().FirstOrDefault().GameId));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NoContentResult)await gameController.DeleteGame(GameMockData.Get().FirstOrDefault().GameId);
            /// Assert
            result.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGame_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _gameService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(GameMockData.Delete(It.IsAny<int>()));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (BadRequestObjectResult)await gameController.DeleteGame(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGame_ShouldReturn404Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(GameMockData.Delete(GameMockData.Get().Count() + 1));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NotFoundObjectResult)await gameController.DeleteGame(GameMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
