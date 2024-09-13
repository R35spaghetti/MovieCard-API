using AutoMapper;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Resolver;

public class GenreResolver : IValueResolver<MovieCreateDTO, Movie, ICollection<Genre>>
{
    private readonly IMapper _mapper;

    public GenreResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ICollection<Genre> Resolve(MovieCreateDTO source, Movie destination, ICollection<Genre> destMember, ResolutionContext context)
    {
        if (source.Genres == null || !source.Genres.Any())
        {
            return new List<Genre>();
        }

        var genres = source.Genres.Select(genreCreateDto => genreCreateDto).ToList();
        return _mapper.Map<ICollection<Genre>>(genres);
    }

  
}