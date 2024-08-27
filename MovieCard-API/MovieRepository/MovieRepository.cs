using Microsoft.AspNetCore.Mvc;
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
                Director: m.Director,
                Actors: m.Actors,
                Genres: m.Genres,
                Title: m.Title,
                Rating: m.Rating,
                ReleaseDate: m.ReleaseDate,
                Description: m.Description
            )).ToList();

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
            Director = createMovie.Director,
            Actors = createMovie.Actors,
            Genres = createMovie.Genres,
            Title = createMovie.Title,
            Rating = createMovie.Rating,
            ReleaseDate = createMovie.ReleaseDate,
            Description = createMovie.Description
        };


        await _context.AddAsync(movie);
        await _context.SaveChangesAsync();
        return createMovie;
    }

    public async Task<MovieDTO> UpdateMovieAsync(MovieDTO updateMovie, int id)
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
                movie.Director = updateMovie.Director;
            }

            movie.Actors = updateMovie.Actors?.Select(a => new Actor
                { Id = a.Id, Name = a.Name, Birthday = a.Birthday, Movies = a.Movies }).ToList();

            movie.Genres = updateMovie.Genres?.Select(g =>
                new Genre { Id = g.Id, Name = g.Name, Movies = g.Movies }).ToList();
            await _context.SaveChangesAsync();
            return updateMovie;
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