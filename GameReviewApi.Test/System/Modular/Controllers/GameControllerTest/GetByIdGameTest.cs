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
    public class GetByIdGameTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGame_ShouldReturn200Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>()))
                .ReturnsAsync(GameMockData.GetById(GameMockData.Get().FirstOrDefault().GameId));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController
                .GetByIdGame(GameMockData.Get().FirstOrDefault().GameId);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGame_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _gameService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>()))
                .ReturnsAsync(GameMockData.GetById(It.IsAny<int>()));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (BadRequestObjectResult)await gameController.GetByIdGame(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGame_ShouldReturn404Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>()))
                .ReturnsAsync(GameMockData.GetById(It.IsAny<int>()));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NotFoundObjectResult)await gameController
                .GetByIdGame(GameMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
