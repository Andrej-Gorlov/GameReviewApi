using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Paging;
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
            GameParameters ownerParameters = new();
            _gameService.Setup(_ => _.GamesAvgGradeAsyncService(ownerParameters)).ReturnsAsync(GameMockData.GameAvgGrade());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = (OkObjectResult)await gameController.GetGames(ownerParameters);
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
            GameParameters ownerParameters = new();
            _gameService.Setup(_ => _.GamesAvgGradeAsyncService(ownerParameters)).ReturnsAsync(GameMockData.GameAvgGrade());
            GameController gameController = new GameController(_gameService.Object);
            /// Act
            var result = await gameController.GetGames(ownerParameters);
            /// Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PagedList<GameAvgGrade>>(viewResult.Value);
            Assert.Equal(GameMockData.GameAvgGrade().Count(), model.Count());
        }
    }
}
