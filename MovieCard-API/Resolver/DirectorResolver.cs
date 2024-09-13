using AutoMapper;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.Resolver;

public class DirectorResolver : IValueResolver<MovieCreateDTO, Movie, Director>
{
    
    private readonly IMapper _mapper;

    public DirectorResolver(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public Director Resolve(MovieCreateDTO source, Movie destination, Director destMember, ResolutionContext context)
    {
        if (source.DirectorId <= 0)
        {
            return new Director();
        }

        var directorId = source.DirectorId.Value;
        return _mapper.Map<Director>(directorId);
    }
}