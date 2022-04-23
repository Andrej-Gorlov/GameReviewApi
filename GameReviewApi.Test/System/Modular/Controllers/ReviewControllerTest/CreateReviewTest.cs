using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.ReviewControllerTest
{
    public class CreateReviewTest
    {
        private readonly Mock<IReviewService> _reviewService = new Mock<IReviewService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 201
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateReview_ShouldReturn201Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.CreateAsyncService(It.IsAny<ReviewDto>())).ReturnsAsync(ReviewMockData.Entity());
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (CreatedAtActionResult)await reviewController.CreateReview(ReviewMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(201);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateReview_ShouldReturn400Status()
        {
            /// Arrange
            ReviewDto? reviewDto = null;
            _reviewService.Setup(_ => _.CreateAsyncService(It.IsAny<ReviewDto>())).ReturnsAsync(reviewDto);
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (BadRequestObjectResult)await reviewController.CreateReview(reviewDto);
            /// Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
