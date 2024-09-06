using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Movie, MovieCreateDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DirectorId, opt => opt.MapFrom(src => src.DirectorId))
            .ForMember(dest => dest.ActorIds, opt => opt.MapFrom(src => src.Actors.Select(a => a.Id).ToList()))
            .ForMember(dest => dest.GenreIds, opt => opt.MapFrom(src => src.Genres.Select(g => g.Id).ToList()));

        CreateMap<MovieCreateDTO, Movie>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DirectorId, opt => opt.MapFrom(src => src.DirectorId))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.GenreIds))
            .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.ActorIds))
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