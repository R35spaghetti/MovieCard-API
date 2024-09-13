using AutoMapper;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Resolver;

public class ActorResolver : IValueResolver<MovieCreateDTO, Movie, ICollection<Actor>>
{
    private readonly IMapper _mapper;

    public ActorResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ICollection<Actor> Resolve(MovieCreateDTO source, Movie destination, ICollection<Actor> destMember, ResolutionContext context)
    {
        if (source.Actors == null || !source.Actors.Any())
        {
            return new List<Actor>();
        }

        var actors = source.Actors.Select(actorDto => actorDto).ToList();
        return _mapper.Map<ICollection<Actor>>(actors);
    }
}