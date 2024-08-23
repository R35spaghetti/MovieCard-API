using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCard_API.Data;
using MovieCard_API.DTOs;

namespace MovieCard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieCardContext _movieCardContext;


    public MoviesController(MovieCardContext movieCardContext)
    {
        _movieCardContext = movieCardContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        var movies = await _movieCardContext.Movies
            .Include(d => d.Director)
            .ThenInclude(c => c.ContactInformation)
            .Include(a => a.Actors)
            .Include(g => g.Genres)
            .ToListAsync();

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

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(movieDtOs, options);
        return Ok(jsonString);
    }
}