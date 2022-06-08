using AutoMapper;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi.Test.Helpers
{
    public class SourceMappingProfile : Profile
    {
        public SourceMappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
