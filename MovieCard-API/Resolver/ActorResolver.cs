using AutoMapper;
using MovieCard_API.Data;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Resolver;

public class ActorResolver : IValueResolver<MovieCreateDTO, Movie, ICollection<Actor>>
{
    private readonly MovieCardContext _context;

    public ActorResolver(MovieCardContext context)
    {
        _context = context;
    }

    public ICollection<Actor> Resolve(MovieCreateDTO source, Movie destination, ICollection<Actor> destMember, ResolutionContext context)
    {
        if (source.ActorIds == null || !source.ActorIds.Any())
        {
            return new List<Actor>();
        }

        var actors = _context.Actors.Where(actor => source.ActorIds.Contains(actor.Id)).ToList();
        return actors;
    }
}