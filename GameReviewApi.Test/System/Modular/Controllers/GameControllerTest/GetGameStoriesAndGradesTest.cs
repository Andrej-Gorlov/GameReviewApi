using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GameControllerTest
{
    public class GetGameStoriesAndGradesTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGameStoriesAndGrades_ShouldReturn200Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.GameStoriesAndGradesAsyncService(It.IsAny<string>()))
                .ReturnsAsync(GameMockData.GameStoriesAndGrades(GameMockData.Get().FirstOrDefault().GameName));
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController
                .GetGameStoriesAndGrades(GameMockData.Get().FirstOrDefault().GameName);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGameStoriesAndGrades_ShouldReturn404Status()
        {
            /// Arrange
            ReviewsByGam? reviewsByGam = null;
            _gameService.Setup(_ => _.GameStoriesAndGradesAsyncService(It.IsAny<string>()))
                .ReturnsAsync(reviewsByGam);
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (NotFoundObjectResult)await gameController
                .GetGameStoriesAndGrades(new string(GameMockData.Entity().GameName.Reverse().ToArray()));
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
