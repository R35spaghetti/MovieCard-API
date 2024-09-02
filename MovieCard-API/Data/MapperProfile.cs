using AutoMapper;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Movie, MovieDTO>().ReverseMap();
        CreateMap<Movie, MovieDTO>().ConstructUsing(
            src => new MovieDTO(src.Director, src.Actors, src.Genres, src.Title,src.Rating, src.ReleaseDate,src.Description));
    }
}