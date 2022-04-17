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
    public class GetByIdReviewTest
    {
        private readonly Mock<IReviewService> _reviewService = new Mock<IReviewService>();

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 200
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdReview_ShouldReturn200Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>()))
                .ReturnsAsync(ReviewMockData.GetById(ReviewMockData.Get().FirstOrDefault().ReviewId));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (OkObjectResult)await reviewController
                .GetByIdReview(ReviewMockData.Get().FirstOrDefault().ReviewId);
            /// Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 400
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdReview_ShouldReturn400Status()
        {
            /// Arrange
            int id = 0;
            _reviewService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>())).ReturnsAsync(ReviewMockData.GetById(It.IsAny<int>()));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (BadRequestObjectResult)await reviewController.GetByIdReview(id);
            /// Assert
            result.StatusCode.Should().Be(400);
        }

        /// <summary>
        /// Проверяет что обработчик возвращает кода состояния 404
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetByIdReview_ShouldReturn404Status()
        {
            /// Arrange
            _reviewService.Setup(_ => _.GetByIdAsyncService(It.IsAny<int>()))
                .ReturnsAsync(ReviewMockData.GetById(It.IsAny<int>()));
            ReviewController reviewController = new ReviewController(_reviewService.Object);
            /// Act
            var result = (NotFoundObjectResult)await reviewController
                .GetByIdReview(ReviewMockData.Get().Count() + 1);
            /// Assert
            result.StatusCode.Should().Be(404);
        }
    }
}
