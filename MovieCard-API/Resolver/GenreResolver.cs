using AutoMapper;
using MovieCard_API.Data;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Resolver;

public class GenreResolver : IValueResolver<MovieCreateDTO, Movie, ICollection<Genre>>
{
    private readonly MovieCardContext _context;

    public GenreResolver(MovieCardContext context)
    {
        _context = context;
    }

    public ICollection<Genre> Resolve(MovieCreateDTO source, Movie destination, ICollection<Genre> destMember, ResolutionContext context)
    {
        if (source.GenreIds == null || !source.GenreIds.Any())
        {
            return new List<Genre>();
        }

        var genres = _context.Genres.Where(genre => source.GenreIds.Contains(genre.Id)).ToList();
        return genres;
    }
}