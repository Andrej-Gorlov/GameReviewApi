using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Paging;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GameControllerTest
{
    public class GetGamesByGenreTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGamesByGenre_ShouldReturn200Status()
        {
            /// Arrange
            GameParameters gameParameters = new();
            _gameService.Setup(_ => _.GamesByGenreAsyncService(It.IsAny<string>(), gameParameters))
                .ReturnsAsync(GameMockData.GamesByGenre(GenreMockData.Get().FirstOrDefault().GenreName));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController
                .GetGamesByGenre(GenreMockData.Get().FirstOrDefault().GenreName, gameParameters);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGamesByGenre_ShouldReturn404Status()
        {
            /// Arrange
            GameParameters gameParameters = new();
            PagedList<string>? games= null;
            _gameService.Setup(_ => _.GamesByGenreAsyncService(It.IsAny<string>(), gameParameters))
                .ReturnsAsync(games);
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NotFoundObjectResult)await gameController
                .GetGamesByGenre(new string(GenreMockData.Get().FirstOrDefault().GenreName.Reverse().ToArray()), gameParameters);
            /// Assert
            result.StatusCode.Should().Be(404);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGamesByGenre_ShouldReturnCorrect()
        {
            /// Arrange
            GameParameters gameParameters = new();
            _gameService.Setup(_ => _.GamesByGenreAsyncService(It.IsAny<string>(),gameParameters))
                .ReturnsAsync(GameMockData.GamesByGenre(GenreMockData.Get().FirstOrDefault().GenreName));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = await gameController
                .GetGamesByGenre(GenreMockData.Get().FirstOrDefault().GenreName, gameParameters);
            /// Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.Value);
            Assert.Equal(GameMockData.GamesByGenre(GenreMockData.Get().FirstOrDefault().GenreName).Count(), model.Count());
        }
    }
}
