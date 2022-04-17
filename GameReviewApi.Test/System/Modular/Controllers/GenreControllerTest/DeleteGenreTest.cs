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
    public class DeleteGenreTest
    {
        private readonly Mock<IGenreService> _genreService = new Mock<IGenreService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 204
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGenre_ShouldReturn204Status()
        {
            /// Arrange
            _genreService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.Delete(GenreMockData.Get().FirstOrDefault().GenreId));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (NoContentResult)await genreController.DeleteGenre(GenreMockData.Get().FirstOrDefault().GenreId);
            /// Assert
            result.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGenre_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _genreService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.Delete(It.IsAny<int>()));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (BadRequestObjectResult)await genreController.DeleteGenre(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteGenre_ShouldReturn404Status()
        {
            /// Arrange
            _genreService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(GenreMockData.Delete(GenreMockData.Get().Count() + 1));
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (NotFoundObjectResult)await genreController.DeleteGenre(GenreMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
