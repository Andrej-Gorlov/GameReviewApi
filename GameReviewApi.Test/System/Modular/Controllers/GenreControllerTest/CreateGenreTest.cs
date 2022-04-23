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
    public class CreateGenreTest
    {
        private readonly Mock<IGenreService> _genreService = new Mock<IGenreService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 201
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateGenre_ShouldReturn201Status()
        {
            /// Arrange
            _genreService.Setup(_ => _.CreateAsyncService(It.IsAny<GenreDto>())).ReturnsAsync(GenreMockData.Entity());
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (CreatedAtActionResult)await genreController.CreateGenre(GenreMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(201);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateGenre_ShouldReturn400Status()
        {
            /// Arrange
            GenreDto? genreDto = null;
            _genreService.Setup(_ => _.CreateAsyncService(It.IsAny<GenreDto>())).ReturnsAsync(genreDto);
            GenreController genreController = new GenreController(_genreService.Object);
            /// Act
            var result = (BadRequestObjectResult)await genreController.CreateGenre(genreDto);
            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
