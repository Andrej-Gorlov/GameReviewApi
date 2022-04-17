using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GameControllerTest
{
    public class GetGamesTest
    {
        private readonly Mock<IGameService> _gameService = new Mock<IGameService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ShouldReturn200Status()
        {
            /// Arrange
            _gameService.Setup(_ => _.GamesAvgGradeAsyncService()).ReturnsAsync(GameMockData.GameAvgGrade());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController.GetGames();
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает корректный результат
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetGames_ShouldReturnCorrect()
        {
            /// Arrange
            _gameService.Setup(_ => _.GamesAvgGradeAsyncService()).ReturnsAsync(GameMockData.GameAvgGrade());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = await gameController.GetGames();
            /// Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GameAvgGrade>>(viewResult.Value);
            Assert.Equal(GameMockData.GameAvgGrade().Count(), model.Count());
        }
    }
}
