using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity.Dto;
using GameReviewApi.Service.Interfaces;

namespace GameReviewApi.Service.Implementations
{
    public class GenreService: IGenreService
    {
        private IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository) => _genreRepository = genreRepository;
        public async Task<GenreDto> CreateAsyncService(GenreDto entity) => await _genreRepository.Create(entity);
        public async Task<bool> DeleteAsyncService(int id) => await _genreRepository.Delete(id);
        public async Task<GenreDto> GetByIdAsyncService(int id) => await _genreRepository.GetById(id);
        public async Task<GenreDto> UpdateAsyncService(GenreDto entity) => await _genreRepository.Update(entity);
    }
}
