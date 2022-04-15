using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class ReviewService: IReviewService
    {
        private IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository) =>_reviewRepository = reviewRepository;
        public async Task<ReviewDto> CreateAsyncService(ReviewDto entity) => await _reviewRepository.Create(entity);
        public async Task<bool> DeleteAsyncService(int id) => await _reviewRepository.Delete(id);
        public async Task<ReviewDto> GetByIdAsyncService(int id) => await _reviewRepository.GetById(id);
        public async Task<ReviewDto> UpdateAsyncService(ReviewDto entity) => await _reviewRepository.Update(entity);
    }
}
