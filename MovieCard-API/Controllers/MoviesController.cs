using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MovieCard_API.DTOs;

namespace MovieCard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieRepository.MovieRepository _movieRepository;

    public MoviesController(MovieRepository.MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(movies, options);
        return Ok(jsonString);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(int id)
    {
        var movie = await _movieRepository.GetMovieByIdAsync(id);

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        var jsonString = JsonSerializer.Serialize(movie, options);
        return Ok(jsonString);
    }
}