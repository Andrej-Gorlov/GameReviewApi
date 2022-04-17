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
    public class UpdateReviewTest
    {
        private readonly Mock<IReviewService> _reviewService = new Mock<IReviewService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateReview_ShouldReturn200Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.UpdateAsyncService(It.IsAny<ReviewDto>())).ReturnsAsync(ReviewMockData.Entity());
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (OkObjectResult)await reviewController.UpdateReview(ReviewMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateReview_ShouldReturn404Status()
        {
            /// Arrange
            ReviewDto reviewDto = new();
            _reviewService.Setup(_ => _.UpdateAsyncService(It.IsAny<ReviewDto>())).ReturnsAsync(reviewDto);
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (NotFoundObjectResult)await reviewController.UpdateReview(ReviewMockData.Entity());
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
