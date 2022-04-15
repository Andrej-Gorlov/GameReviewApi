using AutoMapper;
using GameReviewApi.Domain.Entity;
using GameReviewApi.Domain.Entity.Authenticate;
using GameReviewApi.Domain.Entity.Dto;

namespace GameReviewApi
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(x => {
                x.CreateMap<Game, GameDto>().ReverseMap();
                x.CreateMap<Genre, GenreDto>().ReverseMap();
                x.CreateMap<Review, ReviewDto>().ReverseMap();
                x.CreateMap<User, UserDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
