using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Movie, MovieDTO>().ReverseMap();
        CreateMap<MovieDTO, Movie>();
        CreateMap<Movie, MovieDTO>();
        CreateMap<Movie, MovieCreateDTO>();
        CreateMap<MovieCreateDTO, Movie>();
        CreateMap<MovieCreateDTO, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorIds))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.GenreIds));
        CreateMap<Movie, MovieCreateDTO>()
            .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src => src.Genres))
            .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(src => src.Actors));
   CreateMap<MovieDTO, Movie>()
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Id));
     

        CreateMap<JsonPatchDocument<MovieDTO>, JsonPatchDocument<Movie>>().ReverseMap();

        CreateMap<Director, DirectorDTO>().ReverseMap();
        CreateMap<Director, DirectorCreateDTO>().ReverseMap();

        CreateMap<Actor, ActorDTO>().ReverseMap();
        CreateMap<Actor, ActorCreateDTO>();

        CreateMap<Genre, GenreDTO>().ReverseMap();
        CreateMap<Genre, GenreCreateDTO>().ReverseMap();

        CreateMap<ContactInformation, ContactInformationDTO>().ReverseMap();
        CreateMap<ContactInformation, ContactInformationCreateDTO>().ReverseMap();
    }
}