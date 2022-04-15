using AutoMapper;
using GameReviewApi.DAL.Interfaces;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Dto;
using Microsoft.EntityFrameworkCore;

namespace GameReviewApi.DAL.Repository
{
    public class GenreRepository: IGenreRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public GenreRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;    
        }
        public async Task<GenreDto> Create(GenreDto entity)
        {
            Genre genre = _mapper.Map<GenreDto, Genre>(entity);
            _db.Genre.Add(genre);
            await _db.SaveChangesAsync();
            return _mapper.Map<Genre, GenreDto>(genre);
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                Genre genre = await _db.Genre.FirstOrDefaultAsync(x => x.GenreId == id);
                if (genre == null) 
                {
                    return false;
                } 
                _db.Genre.Remove(genre);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<GenreDto> GetById(int id) => 
            _mapper.Map<GenreDto>(await _db.Genre.FirstOrDefaultAsync(x => x.GenreId == id));
        public async Task<GenreDto> Update(GenreDto entity)
        {
            Genre genre = _mapper.Map<GenreDto, Genre>(entity);
            _db.Genre.Update(genre);
            await _db.SaveChangesAsync();
            return _mapper.Map<Genre, GenreDto>(genre);
        }
    }
}
