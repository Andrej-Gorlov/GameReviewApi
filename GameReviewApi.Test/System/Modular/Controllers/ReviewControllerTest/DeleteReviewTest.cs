using FluentAssertions;
using GameReviewApi.Controllers;
using GameReviewApi.Service.Interfaces;
using GameReviewApi.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameReviewApi.Test.System.Modular.Controllers.ReviewControllerTest
{
    public class DeleteReviewTest
    {
        private readonly Mock<IReviewService> _reviewService = new Mock<IReviewService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 204
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteReview_ShouldReturn204Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(ReviewMockData.Delete(ReviewMockData.Get().FirstOrDefault().ReviewId));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (NoContentResult)await reviewController.DeleteReview(ReviewMockData.Get().FirstOrDefault().ReviewId);
            /// Assert
            result.StatusCode.Should().Be(204);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteReview_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _reviewService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(ReviewMockData.Delete(It.IsAny<int>()));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (BadRequestObjectResult)await reviewController.DeleteReview(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteReview_ShouldReturn404Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.DeleteAsyncService(It.IsAny<int>())).ReturnsAsync(ReviewMockData.Delete(ReviewMockData.Get().Count() + 1));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (NotFoundObjectResult)await reviewController.DeleteReview(ReviewMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
