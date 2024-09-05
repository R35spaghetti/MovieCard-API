using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.DTOs;
using MovieCard_API.Models;
using MovieCard_API.Repositories.contracts;

namespace MovieCard_API.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieCardContext _context;
    private readonly IMapper _mapper;


    public MovieRepository(MovieCardContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MovieDTO>> GetAllMoviesAsync()
    {
        var movies = await _context.Movies
            .Include(d => d.Director)
            .ThenInclude(c => c.ContactInformation)
            .Include(a => a.Actors)
            .Include(g => g.Genres)
            .ToListAsync();

        if (movies == null)
        {
            throw new InvalidOperationException($"Movies were not found");
        }
        
        return _mapper.Map<List<MovieDTO>>(movies);
    }

    public async Task<MovieDTO> GetMovieByIdAsync(int id)
    {
        var movie = await _context.Movies
            .Include(d => d.Director)
            .ThenInclude(c => c.ContactInformation)
            .Include(a => a.Actors)
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with ID {id} not found");
        }

        return _mapper.Map<MovieDTO>(movie);
    }
    public async Task<MovieCreateDTO> CreateMovieAsync(MovieCreateDTO createMovie)
    {
        var movie = _mapper.Map<Movie>(createMovie);
        
        await _context.AddAsync(movie);
        await _context.SaveChangesAsync();
        return _mapper.Map<MovieDTO>(movie);
            return _mapper.Map<MovieCreateDTO>(movie);
    }
    
    public async Task<MovieDTO?> UpdateMovieAsync(MovieDTO updateMovie, int id)
    {
        var movie = await _context.Movies
            .Include(d => d.Director)
            .ThenInclude(c => c.ContactInformation)
            .Include(a => a.Actors)
            .Include(g => g.Genres).FirstOrDefaultAsync(m => m.Id == id);


        if (movie == null)
        {
            throw new InvalidOperationException("Movie not found");
        }

        try
        {
            movie.Title = updateMovie.Title;
            movie.Rating = updateMovie.Rating;
            movie.ReleaseDate = updateMovie.ReleaseDate;
            movie.Description = updateMovie.Description;

            if (updateMovie is {Director: not null })
            {
                movie.Director = _mapper.Map<Director>(updateMovie.Director);
            }

            movie.Actors = _mapper.Map<List<Actor>>(updateMovie.Actors);
            movie.Genres = _mapper.Map<List<Genre>>(updateMovie.Genres);
            
            await _context.SaveChangesAsync();
            return _mapper.Map<MovieDTO>(movie);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

        return null;
    }
    public async Task DeleteMovieAsync(int id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();
    }
}