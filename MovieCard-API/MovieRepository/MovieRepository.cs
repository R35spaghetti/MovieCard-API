using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.DTOs;
using MovieCard_API.Models;

namespace MovieCard_API.MovieRepository;

public class MovieRepository
{
    private readonly MovieCardContext _context;

    public MovieRepository(MovieCardContext context)
    {
        _context = context;
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

        var movieDtOs = movies.Select(m =>
            new MovieDTO(
                Id: m.Id,
                DirectorId: m.DirectorId,
                Director: m.Director,
                Actors: m.Actors,
                Genres: m.Genres,
                Title: m.Title,
                Rating: m.Rating,
                ReleaseDate: m.ReleaseDate,
                Description: m.Description)).ToList();

        return movieDtOs;
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
        
        return new MovieDTO(
            Id: movie.Id,
            DirectorId: movie.DirectorId,
            Director: movie.Director,
            Actors: movie.Actors,
            Genres: movie.Genres,
            Title: movie.Title,
            Rating: movie.Rating,
            ReleaseDate: movie.ReleaseDate,
            Description: movie.Description
        );
    }
    
    public async Task<MovieDTO> CreateMovieAsync(MovieDTO createMovie)
    {
        
        var movie = new Movie
        {
            Id = createMovie.Id,
            DirectorId = createMovie.DirectorId,
            Director = createMovie.Director,
            Actors = createMovie.Actors,
            Genres = createMovie.Genres,
            Title = createMovie.Title,
            Rating = createMovie.Rating,
            ReleaseDate = createMovie.ReleaseDate,
            Description = createMovie.Description,
        };
        
        
        await _context.AddAsync(movie);
        await _context.SaveChangesAsync();
        return createMovie;
    }
}
