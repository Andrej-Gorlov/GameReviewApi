using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GenreControllerTest
{
    public class GetByIdGenreTest
    {
        private readonly Mock<IGenreService> _genreService = new Mock<IGenreService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGenre_ShouldReturn200Status()
        {
            /// Arrange
            int id = 1;
            _genreService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.GetById(id));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (OkObjectResult)await genreController.GetByIdGenre(id);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGenre_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _genreService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.GetById(It.IsAny<int>()));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (BadRequestObjectResult)await genreController.GetByIdGenre(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdGenre_ShouldReturn404Status()
        {
            /// Arrange
            _genreService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.GetById(It.IsAny<int>()));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (NotFoundObjectResult)await genreController.GetByIdGenre(GenreMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
