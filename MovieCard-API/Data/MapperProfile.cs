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
            src => new MovieDTO(src.Director, src.Actors, src.Genres, src.Title, src.Rating, src.ReleaseDate,
                src.Description));
        CreateMap<Actor, ActorDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

        CreateMap<Director, Director>().ReverseMap();
        CreateMap<Director, DirectorDTO>()
            .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));

        CreateMap<Genre, GenreDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies));
    }
}