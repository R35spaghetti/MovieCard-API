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

    public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
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

        return _mapper.Map<IEnumerable<Movie>>(movies);
    }

    public async Task<Movie> GetMovieByIdAsync(int id)
    {
        var movie = await _context.Movies
            // .Include(d => d.Director)
            // .ThenInclude(c => c.ContactInformation)
            // .Include(a => a.Actors)
            // .Include(g => g.Genres)
            .FirstOrDefaultAsync(x => x.Id == id);


        if (movie == null)
        {
            throw new InvalidOperationException($"Movie with ID {id} not found");
        }

        return _mapper.Map<Movie>(movie);
    }

    public async Task<Movie> CreateMovieAsync(MovieCreateDTO createMovie)
    {
        var movie = _mapper.Map<Movie>(createMovie);
        await _context.AddAsync(movie);
        return movie;
    }


    public async Task<Movie> UpdateMovieAsync(MovieDTO updateMovie)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == updateMovie.Id);
        // .Include(d => d.Director)
        // .ThenInclude(c => c.ContactInformation)
        // .Include(a => a.Actors)
        // .Include(g => g.Genres).FirstOrDefaultAsync(m => m.Id == updateMovie.Id);


        if (movie == null)
        {
            throw new InvalidOperationException("Movie not found");
        }

        try
        {
            _mapper.Map(updateMovie, movie);
            _context.Entry(movie).State = EntityState.Modified;
            return movie;
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
        if (movie != null) _context.Movies.Remove(movie);
    }
}