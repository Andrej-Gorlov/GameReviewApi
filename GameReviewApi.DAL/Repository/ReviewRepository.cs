using AutoMapper;
using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApi.DAL.Repository
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ReviewRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;  
        }
       
        public async Task<ReviewDto> Create(ReviewDto entity)
        {
            Review review = _mapper.Map<ReviewDto, Review>(entity);
            _db.Review.Add(review);
           await _db.SaveChangesAsync();
            return _mapper.Map<Review, ReviewDto>(review);
        }
       
        public async Task<bool> Delete(int id)
        {
            try
            {
                Review review = await _db.Review.FirstOrDefaultAsync(x => x.ReviewId == id);
                if (review == null) 
                {
                    return false;
                } 
                _db.Review.Remove(review);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ReviewDto> GetById(int id) =>
            _mapper.Map<ReviewDto>(await _db.Review.FirstOrDefaultAsync(x => x.ReviewId == id));
       
        public async Task<ReviewDto> Update(ReviewDto entity)
        {
            Review review = _mapper.Map<ReviewDto, Review>(entity);
            if (await _db.Review.AsNoTracking().FirstOrDefaultAsync(x => x.ReviewId == entity.ReviewId) is null)
            {
                throw new NullReferenceException("Попытка обновить объект, которого нет в хранилище.");
            }
            _db.Review.Update(review);
            await _db.SaveChangesAsync();
            return _mapper.Map<Review, ReviewDto>(review);
        }
    }
}
