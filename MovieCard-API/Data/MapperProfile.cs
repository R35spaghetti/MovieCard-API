using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Data;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<MovieCreateDTO, Movie>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<MovieDTO, Movie>();
        
        CreateMap<ActorCreateDTO, Actor>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<ActorDTO, Actor>();


        CreateMap<JsonPatchDocument<MovieDTO>, JsonPatchDocument<Movie>>();
        CreateMap<DirectorDTO, Director>();
        CreateMap<DirectorCreateDTO, Director>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<GenreDTO, Genre>();
        CreateMap<GenreCreateDTO, Genre>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());


        CreateMap<ContactInformationDTO, ContactInformation>();
        CreateMap<ContactInformationCreateDTO, ContactInformation>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}