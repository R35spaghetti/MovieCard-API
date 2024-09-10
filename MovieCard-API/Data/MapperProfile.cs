using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MovieCard_API.DTOs;
using MovieCard_API.Models;
using MovieCard_API.Resolver;

namespace MovieCard_API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<MovieCreateDTO, Movie>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom<GenreResolver>())
            .ForMember(dest => dest.Actors, opt => opt.MapFrom<ActorResolver>())
            .ForMember(dest => dest.Actors, opt => opt.Ignore())
            .ForMember(dest => dest.Genres, opt => opt.Ignore());

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