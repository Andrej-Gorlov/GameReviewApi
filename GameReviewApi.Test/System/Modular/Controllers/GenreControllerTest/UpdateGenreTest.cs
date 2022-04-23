using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.GenreControllerTest
{
    public class UpdateGenreTest
    {
        private readonly Mock<IGenreService> _genreService = new Mock<IGenreService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateGenre_ShouldReturn200Status()
        {
            /// Arrange
            _genreService.Setup(_ => _.UpdateAsyncService(It.IsAny<GenreDto>())).ReturnsAsync(GenreMockData.Entity());
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (OkObjectResult)await genreController.UpdateGenre(GenreMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateGenre_ShouldReturn404Status()
        {
            /// Arrange
            GenreDto? reviewDto = null;
            _genreService.Setup(_ => _.UpdateAsyncService(It.IsAny<GenreDto>())).ReturnsAsync(reviewDto);
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (NotFoundObjectResult)await genreController.UpdateGenre(GenreMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
