using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.DTOs;
using MovieCard_API.Models;
using MovieCard_API.Repositories.contracts;

namespace MovieCard_API.Repositories;

public class ActorRepository : IActorRepository
{
    
    private readonly MovieCardContext _context;
    private readonly IMapper _mapper;


    public ActorRepository(MovieCardContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task AddActorToMovieAsync(int movieId, ICollection<ActorCreateDTO> actors)
    {
        var movie = _context.Movies.FirstOrDefault(p => p.Id == movieId);

        if (movie == null)
        {
            throw new Exception($"Movie with id {movieId} not found.");
        }

        var existingActors = await _context.Actors
            .Where(a => a.Id == movieId)
            .ToListAsync();


        foreach (Actor actor in (from actor in actors
                     let existingActor =
                         existingActors.FirstOrDefault(a => a.Name == actor.Name && a.Birthday == actor.Birthday)
                     select existingActor).OfType<Actor>())
        {
            throw new Exception($"{actor.Name} with birthday {actor.Birthday} already exists.");
        }

        _mapper.Map<Actor>(actors);

    }
}

